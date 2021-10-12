using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cache
{
    public interface IRedisStorage
    {
        Task<bool> LockTake(string key, string value, TimeSpan timeout);
        Task<bool> LockRelease(string key, string value);
        Task<string> LockQuery(string key);
        Task<bool> StringSet(string key, object value);
        Task<bool> StringSet(string key, object value, TimeSpan timeout);
        Task<long> StringIncrement(string key, long value, TimeSpan timeout);
        Task<bool> KeyDelete(string key);
        Task<bool> HashExists(string key, string field);
        Task<T> StringGet<T>(string key);
        Task<T[]> StringGet<T>(string[] keys);
        Task<long> ListLeftPush(string key, object value);
        Task<bool> HashSet(string key, string field, object value);
        Task<long> HashIncrement(string key, string field, long value);
        Task<long?> HashIncrement(string key, string field, long value, long minValue);
        Task<KeyValuePair<string, T>[]> HashGetAll<T>(string key);
        Task<KeyValuePair<string, KeyValuePair<string, T>[]>[]> HashGetAll<T>(string[] keys);
        Task<KeyValuePair<string, KeyValuePair<string, long>[]>[]> HashGetAll(string[] keys);
        Task<KeyValuePair<string, long>[]> HashGetAll(string key);
        Task<T> HashGet<T>(string key, string field);
        Task<T[]> HashGet<T>(string key, string[] fields);
        Task<bool> HashDelete(string key, string field);
        Task<bool> KeyExist(string key);
    }
}