using Newtonsoft.Json;
using StackExchange.Redis;

namespace CommonHelpers
{
    public class RedisHelper
    {
        private IConnectionMultiplexer connectionMultiplexer;
        public IDatabase database;

        public RedisHelper(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
            database = connectionMultiplexer.GetDatabase();
        }

        public bool AddList<T>(string key, IEnumerable<T> values)
        {
            var value = values.Select(x => JsonConvert.SerializeObject(x));
            IEnumerable<RedisValue> redisValues = value.Select(x =>new RedisValue(x));
            database.ListRightPush(key, redisValues.ToArray());
            return true;
        }

        

    }
}
