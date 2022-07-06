using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus
{
    public class RabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;

        private bool _disposed;
        private readonly object sync_root = new object();

        public RabbitMQConnection(IConnectionFactory connectionFactory, IConnection connection)
        {
            _connectionFactory = connectionFactory;
            _connection = connection;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("未找到对应的MQ连接");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _connection.Dispose();
        }

        public bool TryConnect()
        {
            lock (sync_root)
            {
                _connection = _connectionFactory.CreateConnection();
                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutDown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        #region 事件
        private void OnConnectionBlocked(object sender,ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }

        private void OnCallbackException(object sender,CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }

        private void OnConnectionShutDown(object sender,ShutdownEventArgs eventArgs)
        {
            if (_disposed) return;
            TryConnect();
        }
        #endregion
    }
}
