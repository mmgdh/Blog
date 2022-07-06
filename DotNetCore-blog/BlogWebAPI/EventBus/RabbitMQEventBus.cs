using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace EventBus
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private IModel _consumerChannel;
        private readonly string _exchangeName;
        private string _queueName;
        private readonly RabbitMQConnection _rabbitMQConnection;
        private readonly SubscriptionsManager _subsManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _serviceScope;

        public RabbitMQEventBus(RabbitMQConnection rabbitMQConnection, IServiceScopeFactory serviceScopeFactory, string exchangeName, string queueName)
        {
            _exchangeName = exchangeName;
            _queueName = queueName;
            _rabbitMQConnection = rabbitMQConnection ?? throw new ArgumentNullException(nameof(rabbitMQConnection));
            this._subsManager = new SubscriptionsManager();
            this._serviceScope = serviceScopeFactory.CreateScope();
            this._serviceProvider = _serviceScope.ServiceProvider;
            this._consumerChannel = CreateConsumerChannel();
            this._subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }
        private void SubsManager_OnEventRemoved(object? sender, string eventName)
        {
            if (!_rabbitMQConnection.IsConnected)
            {
                _rabbitMQConnection.TryConnect();
            }

            using (var channel = _rabbitMQConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: _exchangeName,
                    routingKey: eventName);

                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }
        private IModel CreateConsumerChannel()
        {
            if (!_rabbitMQConnection.IsConnected)
            {
                _rabbitMQConnection.TryConnect();
            }

            var channel = _rabbitMQConnection.CreateModel();
            channel.ExchangeDeclare(exchange: _exchangeName, type: "direct");
            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.CallbackException += (sender, args) =>
             {
                 Debug.Fail(args.ToString());
             };
            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    using var scope = this._serviceProvider.CreateScope();
                    IIntegrationEventHandler? handler = scope.ServiceProvider.GetService(subscription) as IIntegrationEventHandler;
                    if (handler == null)
                    {
                        throw new ApplicationException($"无法创建{subscription}类型的服务");
                    }
                    await handler.Handle(eventName, message);
                }
            }
            else
            {
                string entryAsm = Assembly.GetEntryAssembly().GetName().Name;
                Debug.WriteLine($"找不到可以处理eventName={eventName}的处理程序，entryAsm:{entryAsm}");
            }
        }

        private void StartBasicConsume()
        {
            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
                consumer.Received += Consumer_Received;
                _consumerChannel.BasicConsume(queue:_queueName, consumer: consumer,autoAck:false);
            }
        }

        private async Task Consumer_Received(object sender,BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
            try
            {
                await ProcessEvent(eventName, message);
                _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
            }
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }
            _subsManager.Clear();
            this._rabbitMQConnection.Dispose();
            this._serviceScope.Dispose();
        }

        public void publish(string eventName, object? eventData)
        {
            if (!_rabbitMQConnection.IsConnected)
            {
                _rabbitMQConnection.TryConnect();
            }
            //Channel 是建立在 Connection 上的虚拟连接
            //创建和销毁 TCP 连接的代价非常高，
            //Connection 可以创建多个 Channel ，Channel 不是线程安全的所以不能在线程间共享。
            using (var channel = _rabbitMQConnection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _exchangeName, type: "direct");

                byte[] body;
                if (eventData == null)
                {
                    body = new byte[0];
                }
                else
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    body = JsonSerializer.SerializeToUtf8Bytes(eventData, eventData.GetType(), options);
                }
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                channel.BasicPublish(
                    exchange: _exchangeName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }
        }

        public void Subscribe(string eventName, Type handlerType)
        {
            CheckHandlerType(handlerType);
            DoInternalSubscription(eventName);
            _subsManager.AddSubscription(eventName, handlerType);
            StartBasicConsume();
        }

        public void Unsubscribe(string eventName, Type handlerType)
        {
            CheckHandlerType(handlerType);
            _subsManager.RemoveSubscription(eventName, handlerType);
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_rabbitMQConnection.IsConnected)
                {
                    _rabbitMQConnection.TryConnect();
                }
                _consumerChannel.QueueBind(queue: _queueName,
                                    exchange: _exchangeName,
                                    routingKey: eventName);
            }
        }

        private void CheckHandlerType(Type handlerType)
        {
            if (!typeof(IIntegrationEventHandler).IsAssignableFrom(handlerType))
            {
                throw new ArgumentException($"{handlerType} doesn't inherit from IIntegrationEventHandler", nameof(handlerType));
            }
        }
    }
}
