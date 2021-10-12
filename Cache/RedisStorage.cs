using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using StackExchange.Redis;
using ProtoBuf;

namespace Cache
{
    public class RedisStorage: IRedisStorage
    {
        private readonly RedisConnection _redisConnection;
        private IDatabase WriteDatabase => _redisConnection.GetWriteConnection();
        private IDatabase ReadDatabase => _redisConnection.GetReadConnection();

        public RedisStorage(RedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }
        
        public async Task<bool> LockTake(string key, string value, TimeSpan timeout)
        {
            return await WriteDatabase.LockTakeAsync(key, value, timeout);
        }

        public async Task<bool> LockRelease(string key, string value)
        {
            return await WriteDatabase.LockReleaseAsync(key, value);
        }

        public async Task<string> LockQuery(string key)
        {
            return await WriteDatabase.LockQueryAsync(key);
        }

        public async Task<bool> StringSet(string key, object value)
        {
            RedisValue redisValue = ConvertInput(value);
            return await WriteDatabase.StringSetAsync(key, redisValue, null);
        }

        public async Task<bool> StringSet(string key, object value, TimeSpan timeout)
        {
            RedisValue redisValue = ConvertInput(value);
            return await WriteDatabase.StringSetAsync(key, redisValue, timeout);
        }

        public async Task<long> StringIncrement(string key, long value, TimeSpan timeout)
        {
            long increment = await WriteDatabase.StringIncrementAsync(key, value);
            await WriteDatabase.KeyExpireAsync(key, timeout);
            return increment;
        }

        public async Task<bool> KeyDelete(string key)
        {
            return await WriteDatabase.KeyDeleteAsync(key);
        }

        public Task<bool> HashExists(string key, string field)
        {
            throw new NotImplementedException();
        }

        public async Task<T> StringGet<T>(string key)
        {
            RedisValue value = await ReadDatabase.StringGetAsync(key);
            return ConvertOutput<T>(value);
        }

        public async Task<T[]> StringGet<T>(string[] keys)
        {
            RedisKey[] redisKeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                redisKeys[i] = keys[i];
            }

            RedisValue[] values = await ReadDatabase.StringGetAsync(redisKeys);
            T[] results = new T[keys.Length];
            int j = 0;
            foreach (var redisValue in values)
            {
                if (redisValue.HasValue)
                {
                    var obj = ConvertOutput<T>(redisValue);
                    results[j] = obj;
                }

                j++;
            }

            return results;
        }

        public async Task<long> ListLeftPush(string key, object value)
        {
            return await WriteDatabase.ListLeftPushAsync(key, ConvertInput(value));
        }

        public async Task<bool> HashSet(string key, string field, object value)
        {
            RedisValue redisValue = ConvertInput(value);
            var result = await WriteDatabase.HashSetAsync(key, field, redisValue);
            return result;
        }

        public async Task<long> HashIncrement(string key, string field, long value)
        {
            return await WriteDatabase.HashIncrementAsync(key, field, value);
        }

        public async Task<long?> HashIncrement(string key, string field, long value, long minValue)
        {
            try
            {
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<KeyValuePair<string, T>[]> HashGetAll<T>(string key)
        {
            HashEntry[] hashEntries = await ReadDatabase.HashGetAllAsync(key);
            if (hashEntries.Length > 0)
            {
                KeyValuePair<string, T>[] results = new KeyValuePair<string, T>[hashEntries.Length];
                int i = 0;
                foreach (var hashEntry in hashEntries)
                {
                    results[i] =
                        new KeyValuePair<string, T>(hashEntry.Name, ConvertOutput<T>(hashEntry.Value));
                    i++;
                }

                return results;
            }

            return default(KeyValuePair<string, T>[]);
        }

        public Task<KeyValuePair<string, KeyValuePair<string, T>[]>[]> HashGetAll<T>(string[] keys)
        {
            throw new NotImplementedException();
        }

        public Task<KeyValuePair<string, KeyValuePair<string, long>[]>[]> HashGetAll(string[] keys)
        {
            throw new NotImplementedException();
        }

        public async Task<KeyValuePair<string, long>[]> HashGetAll(string key)
        {
            HashEntry[] hashEntries = await ReadDatabase.HashGetAllAsync(key);
            return hashEntries.Select(p => new KeyValuePair<string, long>(p.Name, (long) p.Value)).ToArray();
        }

        public async Task<T> HashGet<T>(string key, string field)
        {
            RedisValue redisValue = await ReadDatabase.HashGetAsync(key, field);
            return ConvertOutput<T>(redisValue);
        }

        public async Task<T[]> HashGet<T>(string key, string[] field)
        {
            RedisValue[] redisValues = field.Select(p => (RedisValue) p).ToArray();
            var values = await ReadDatabase.HashGetAsync(key, redisValues);
            T[] results = new T[values.Length];
            int i = 0;
            foreach (var redisValue in values)
            {
                if (redisValue.HasValue)
                {
                    var obj = ConvertOutput<T>(redisValue);
                    results[i] = obj;
                }

                i++;
            }

            return results;
        }

        public async Task<bool> HashDelete(string key, string field)
        {
            return await WriteDatabase.HashDeleteAsync(key, field);
        }

        public async Task<bool> KeyExist(string key)
        {
            return ReadDatabase.KeyExists(key);
        }

        public async Task<bool> KeyExists(string key, string field)
        {
            return await WriteDatabase.HashExistsAsync(key, field);
        }

        private byte[] ConvertInput(object value)
        {
            return Serialize.ProtoBufSerialize(value);
        }

        private T ConvertOutput<T>(RedisValue redisValue)
        {
            if (redisValue.HasValue)
            {
                return Serialize.ProtoBufDeserialize<T>(redisValue);
            }

            return default(T);
        }
    }
}