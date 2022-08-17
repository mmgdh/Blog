using Newtonsoft.Json;
using StackExchange.Redis;

namespace CommonHelpers
{
    public class RedisHelper
    {
        private IConnectionMultiplexer connectionMultiplexer;
        private IDatabase database;

        public RedisHelper(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
            database = connectionMultiplexer.GetDatabase();
        }

        public bool AddList<T>(IEnumerable<T> values)
        {
            var a = JsonConvert.SerializeObject(values);
            IEnumerable<RedisValue> redisValues = values.Select(x =>new RedisValue(a));

            database.StringGetAsync("");
            database.ListRightPush("", redisValues.ToArray());

            return true;
        }

    }
}
