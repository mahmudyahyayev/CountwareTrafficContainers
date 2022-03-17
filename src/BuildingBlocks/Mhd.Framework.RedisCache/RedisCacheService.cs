using Mhd.Framework.Common;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mhd.Framework.Cache
{
    public class RedisCacheService : ICache, ISingletonSelfDependency
    {
        private readonly RedisConfiguration _configuration;
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisCacheService(RedisConfiguration configuration)
        {
            _configuration = configuration;
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_configuration.Connection));
        }

        private ConnectionMultiplexer Connection => _lazyConnection.Value;

        public T Get<T>(string key)
        {
            IDatabase db = Connection.GetDatabase();

            string redisValue = db.StringGet(key);

            if (!string.IsNullOrWhiteSpace(redisValue))
                return redisValue.Deserialize<T>();

            return default;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            IDatabase db = Connection.GetDatabase();

            string redisValue = await db.StringGetAsync(key);

            if (!string.IsNullOrWhiteSpace(redisValue))
                return redisValue.Deserialize<T>();

            return default;
        }

        public bool Add<T>(string key, T value, TimeSpan? expireTime = null, bool @override = true)
        {
            IDatabase db = Connection.GetDatabase();

            if (value != null)
            {
                var redisValue = value.Serialize();
                return db.StringSet(key, redisValue, expireTime, @override ? When.Always : When.NotExists);
            }

            return false;
        }

        public async Task<bool> AddAsync<T>(string key, T value, TimeSpan? expireTime = null, bool @override = true)
        {
            IDatabase db = Connection.GetDatabase();

            if (value != null)
            {
                var redisValue = value.Serialize();
                return await db.StringSetAsync(key, redisValue, expireTime, @override ? When.Always : When.NotExists);
            }

            return false;
        }

        public bool Delete(string key)
        {
            IDatabase db = Connection.GetDatabase();
            return db.KeyDelete(key);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            IDatabase db = Connection.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }

        public List<T> GetList<T>(string key, long start = 0, long stop = -1)
        {
            IDatabase db = Connection.GetDatabase();

            var redisValues = db.ListRange(key, start, stop);

            var result = new List<T>();

            if (redisValues.Length > 0)
            {
                foreach (string redisValue in redisValues)
                {
                    result.Add(redisValue.Deserialize<T>());
                }
            }

            return result;
        }

        public async Task<List<T>> GetListAsync<T>(string key, long start = 0, long stop = -1)
        {
            IDatabase db = Connection.GetDatabase();

            var redisValues = await db.ListRangeAsync(key, start, stop);

            var result = new List<T>();

            if (redisValues.Length > 0)
            {
                foreach (string redisValue in redisValues)
                {
                    result.Add(redisValue.Deserialize<T>());
                }
            }

            return result;
        }

        public T GetListItem<T>(string key, long index)
        {
            IDatabase db = Connection.GetDatabase();

            string redisValue = db.ListGetByIndex(key, index);

            if (!string.IsNullOrWhiteSpace(redisValue))
                return redisValue.Deserialize<T>();

            return default(T);
        }

        public async Task<T> GetListItemAsync<T>(string key, long index)
        {
            IDatabase db = Connection.GetDatabase();

            string redisValue = await db.ListGetByIndexAsync(key, index);

            if (!string.IsNullOrWhiteSpace(redisValue))
                return redisValue.Deserialize<T>();

            return default(T);
        }

        public void AddList<T>(string key, T value)
        {
            IDatabase db = Connection.GetDatabase();

            if (value != null)
            {
                var redisValue = value.Serialize();
                db.ListRightPush(key, redisValue);
            }
        }

        public async Task AddListAsync<T>(string key, T value)
        {
            IDatabase db = Connection.GetDatabase();

            if (value != null)
            {
                var redisValue = value.Serialize();
                await db.ListRightPushAsync(key, redisValue);
            }
        }

        public void AddRangeList<T>(string key, List<T> values)
        {
            IDatabase db = Connection.GetDatabase();

            RedisValue[] redisValues = new RedisValue[values.Count];

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var redisValue = values[i].Serialize();
                    redisValues[i] = redisValue;
                }

                db.ListRightPush(key, redisValues);
            }
        }

        public async Task AddRangeListAsync<T>(string key, List<T> values)
        {
            IDatabase db = Connection.GetDatabase();

            RedisValue[] redisValues = new RedisValue[values.Count];

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var redisValue = values[i].Serialize();
                    redisValues[i] = redisValue;
                }

                await db.ListRightPushAsync(key, redisValues);
            }
        }

        public void DeleteListItem<T>(string key, T value)
        {
            IDatabase db = Connection.GetDatabase();

            var redisValue = value.Serialize();
            db.ListRemoveAsync(key, redisValue);
        }

        public async Task DeleteListItemAsync<T>(string key, T value)
        {
            IDatabase db = Connection.GetDatabase();

            var redisValue = value.Serialize();
            await db.ListRemoveAsync(key, redisValue);
        }

        public void IncrementString(string key, int value = 1)
        {
            IDatabase db = Connection.GetDatabase();
            db.StringIncrement(key, value);
        }

        public async Task IncrementStringAsync(string key, int value = 1)
        {
            IDatabase db = Connection.GetDatabase();
            await db.StringIncrementAsync(key, value);
        }

        public void DecrementString(string key, int value = 1)
        {
            IDatabase db = Connection.GetDatabase();
            db.StringDecrement(key, value);
        }

        public async Task DecrementStringAsync(string key, int value = 1)
        {
            IDatabase db = Connection.GetDatabase();
            await db.StringDecrementAsync(key, value);
        }

        public bool LockTake(string key, string value, TimeSpan expiryTime)
        {
            IDatabase db = Connection.GetDatabase();
            return db.LockTake(key, value, expiryTime);
        }

        public async Task<bool> LockTakeAsync(string key, string value, TimeSpan expiryTime)
        {
            IDatabase db = Connection.GetDatabase();
            return await db.LockTakeAsync(key, value, expiryTime);
        }

        public bool LockRelease(string key, string value)
        {
            IDatabase db = Connection.GetDatabase();
            return db.LockRelease(key, value);
        }

        public async Task<bool> LockReleaseAsync(string key, string value)
        {
            IDatabase db = Connection.GetDatabase();
            return await db.LockReleaseAsync(key, value);
        }
    }
}
