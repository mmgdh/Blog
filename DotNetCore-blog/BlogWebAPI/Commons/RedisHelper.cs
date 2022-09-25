using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace CommonHelpers
{
    public class RedisHelper
    {
        private IConnectionMultiplexer connectionMultiplexer;
        public int DbNum=0;
        public string RedisKey = "";
        //private TimeSpan DefaultExpire=30000;

        public RedisHelper(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
        }
        #region 扩展方法

        private T Do<T>(Func<IDatabase, T> func)
        {
            var database = connectionMultiplexer.GetDatabase(DbNum);
            return func(database);
        }

        private void Do(Action<IDatabase> func)
        {
            var database = connectionMultiplexer.GetDatabase(DbNum);
            func(database);
        }

        private string ConvertJson<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        private T ConvertObj<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        private List<T> ConvertList<T>(RedisValue[] value)
        {
            List<T> result = new List<T>();
            foreach (var item in value)
            {
                result.Add(ConvertObj<T>(item));
            }
            return result;
        }

        public async Task ReSetRedisValue<T>(string KeyName,List<T>? values = null,Func<Task<List<T>>>? reSetFunc=null)
        {
            //删除后重新设置redis缓存
            //KeyDelete(KeyName);
            if (values == null)
            {
                if (reSetFunc != null)
                    values = await reSetFunc.Invoke();
                else
                    values = new List<T>();
            }
            await AddListAsync<T>(KeyName, values);
            await KeyExpire(KeyName, TimeSpan.FromSeconds(30));
        }
        private RedisKey[] ConvertRedisKeys(List<string> rediskey)
        {
            return rediskey.Select(key => (RedisKey)key).ToArray();
        }
        #endregion

        #region 特殊方法
        public async Task<bool> AddFileCache(string key, byte[] bytes,string fileType)
        {
            string strByte = Convert.ToBase64String(bytes);
            await HashSet(key, "Item1", strByte);
            await HashSet(key, "Item2", fileType);
            KeyExpire(key, new TimeSpan(0, 10, 0));
            return true;
        }
        public Tuple<byte[], string>? GetFileCache(string key)
        {
            var Cache = HashGetAll(key);
            if (Cache == null || Cache.Count() == 0) return null;
            var strbyte = Cache.Where(x=>x.Name=="Item1").First().Value;
            var fileType = Cache.Where(x=>x.Name=="Item2").First().Value;
            if (string.IsNullOrEmpty(strbyte) || string.IsNullOrEmpty(fileType)) return null;
            var bytes = Convert.FromBase64String(strbyte!);
            KeyExpire(key, new TimeSpan(0, 10, 0));
            return new Tuple<byte[], string>(bytes, fileType!);
        }
        #endregion

        #region String
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return Do(db => db.StringGet(key));
        }
        public string StringGet(string key, string CustomKey)
        {
            return Do(db => db.StringGet(CustomKey + key));
        }
        /// <summary>
        /// 新增String键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value)
        {
            return Do(db => db.StringSet(key, value));
        }
        /// <summary>
        /// String键值自增
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IncrKey(string key)
        {
            return Do(db => (db.StringIncrement(key) > 0) ? true : false);
        }
        #endregion

        #region 列表List
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key)
        {
            return Do(redis =>
            {
                var values = redis.ListRange(key);
                return ConvertList<T>(values);
            });
        }
        public async Task<bool> AddListAsync<T>(string key, IEnumerable<T> values)
        {
            var value = values.Select(x => ConvertJson(x));
            IEnumerable<RedisValue> redisValues = value.Select(x => new RedisValue(x));
            await Do(db => db.ListRightPushAsync(key, redisValues.ToArray()));
            return true;
        }
        public async Task<bool> ListPushOneAsync<T>(string key,T value)
        {
            if (KeyExists(key))
            {
              await  Do(db => db.ListRightPushAsync(key, new RedisValue(ConvertJson(value))));
            }
            return true;
        }
        #endregion

        #region Hash 
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            return Do(db => db.HashExists(key, dataKey));
        }
        /// <summary>
        /// 单个值存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> HashSet<T>(string key, string dataKey, T t)
        {
            return await Do(db =>
            {
                string str = t.ToString();
                return db.HashSetAsync(key, dataKey, str);
            });
        }
        /// <summary>
        /// 单个值自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool HashSetIncr(string key, string dataKey, int num)
        {
            return Do(db =>
            {
                return db.HashIncrement(key, dataKey, num) > 0;
            });
        }
        /// <summary>
        /// 单个值自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool HashSetIncr(string key, string dataKey, int num, int isAdd)
        {
            return Do(db =>
            {
                return db.HashIncrement(key, dataKey, num) > 0;
            });
        }
        /// <summary>
        /// 多值(field)存储Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashstr"></param>
        public void HashMSet(string key, HashEntry[] hashstr)
        {
            Do(db => db.HashSet(key, hashstr));
;
        }
        /// <summary>
        /// 获取hash所有键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isAdd">是否需要加上前缀：1是 0否</param>
        /// <returns></returns>
        public HashEntry[] HashGetAll(string key)
        {
            return Do(db =>
            {
                return db.HashGetAll(key);
            });
        }
        public RedisValue[] HashGetAll(string key, RedisValue[] datafiled)
        {
            return Do(db =>
            {
                return db.HashGet(key, datafiled);
            });
        }
        /// <summary>
        /// 获取hash单个键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string HashGet(string key, string dataKey)
        {
            return Do(db =>
            {
                string value = db.HashGet(key, dataKey);
                return db.HashGet(key, dataKey).ToString();
            });
        }

        /// <summary>
        /// 对Hash进行排序
        /// </summary>
        /// <param name="key"></param>
        /// <param name="getfiled"></param>
        /// <returns></returns>
        public RedisValue[] HashSort(string key, string sortfiled, int sortOrder, RedisValue[] getfiled)
        {
            return Do(db =>
            {
                return db.Sort(key, 0, -1, sortOrder == 1 ? Order.Ascending : Order.Descending, SortType.Numeric, sortfiled, getfiled);
            });
        }
        #endregion

        #region key
        /// <summary>
        /// 设置key值过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpire(string key, TimeSpan? expiry)
        {
            if (expiry == null) expiry = new TimeSpan(0, 10, 0);//默认10分钟
            return await Do(db => db.KeyExpireAsync(key, expiry));
        }
        /// <summary>
        /// key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return Do(db => db.KeyExists(key));
        }
        /// <summary>
        /// 模糊查询key名称,获取key集合(!!!注意，使用值时，不要再加前缀)
        /// </summary>
        /// <param name="pattern">关键字</param>
        /// <returns></returns>
        public List<string> GetLikeKey(string pattern)
        {
            pattern = RedisKey + pattern + "*";
            return Do(db =>
            {
                var result = db.ScriptEvaluate(LuaScript.Prepare(" local res= redis.call('KEYS',@keypattern) return res"), new { @keypattern = pattern });
                List<string> list = new List<string>((string[])result);
                return list;
            });
        }
        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            return Do(db => db.KeyDelete(key));
        }
        /// <summary>
        /// 根据关键字，模糊查询key集合，并删除
        /// </summary>
        /// <param name="pattern">关键字</param>
        /// <returns></returns>
        //public long KeysDelete(string pattern)
        //{
        //    return Do(db =>
        //    {
        //        var _server = _instance.GetServer(_instance.GetEndPoints()[DbNum]);
        //        var keys = _server.Keys(db.Database, pattern + "*");
        //        return db.KeyDelete(keys.ToArray());
        //    });
        //}

        /// <summary>
        /// 删除key集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long KeysDelete(List<string> key)
        {
            RedisKey[] value = ConvertRedisKeys(key);
            return Do(db => db.KeyDelete(value));
        }
        #endregion


    }
}
