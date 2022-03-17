using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mhd.Framework.Core
{
    public interface ICache
    {
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);
        bool Add<T>(string key, T value, TimeSpan? expireTime = null, bool @override = true);
        Task<bool> AddAsync<T>(string key, T value, TimeSpan? expireTime = null, bool @override = true);
        bool Delete(string key);
        Task<bool> DeleteAsync(string key);
        List<T> GetList<T>(string key, long start = 0, long stop = -1);
        Task<List<T>> GetListAsync<T>(string key, long start = 0, long stop = -1);
        T GetListItem<T>(string key, long index);
        Task<T> GetListItemAsync<T>(string key, long index);
        void AddList<T>(string key, T value);
        Task AddListAsync<T>(string key, T value);
        void AddRangeList<T>(string key, List<T> values);
        Task AddRangeListAsync<T>(string key, List<T> values);
        void DeleteListItem<T>(string key, T value);
        Task DeleteListItemAsync<T>(string key, T value);
        void IncrementString(string key, int value = 1);
        Task IncrementStringAsync(string key, int value = 1);
        void DecrementString(string key, int value = 1);
        Task DecrementStringAsync(string key, int value = 1);
        bool LockTake(string key, string value, TimeSpan expiryTime);
        Task<bool> LockTakeAsync(string key, string value, TimeSpan expiryTime);
        bool LockRelease(string key, string value);
        Task<bool> LockReleaseAsync(string key, string value);
    }
}