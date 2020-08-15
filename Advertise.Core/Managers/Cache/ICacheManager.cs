using System;

namespace Advertise.Core.Managers.Cache
{
    public interface ICacheManager : IDisposable
    {
        T Get<T>(string key);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
    }
}
