using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Reflection;

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
            //this._consumerChannel = Crea
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
                consumer.Received+=consumer_re
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
            throw new NotImplementedException();
        }

        public void Subscribe(string eventName, Type handlerType)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string eventName, Type handlerType)
        {
            throw new NotImplementedException();
        }
    }
}
