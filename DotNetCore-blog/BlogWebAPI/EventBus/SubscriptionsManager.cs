namespace EventBus
{
    public class SubscriptionsManager
    {
        private readonly Dictionary<string, List<Type>> _handlers = new Dictionary<string, List<Type>>();

        public event EventHandler<string> OnEventRemoved;

        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public void AddSubscription(string eventName,Type eventHandlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }
            if (_handlers[eventName].Contains(eventHandlerType))
            {
                throw new ArgumentException($"事件已注册",nameof(eventHandlerType));
            }
            _handlers[eventName].Add(eventHandlerType);
        }

        public void RemoveSubscription(string eventName,Type handlerType)
        {
            _handlers[eventName].Remove(handlerType);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                OnEventRemoved?.Invoke(this, eventName);
            }
        }

        public IEnumerable<Type> GetHandlersForEvent(string eventName) => _handlers[eventName];
    }
}
