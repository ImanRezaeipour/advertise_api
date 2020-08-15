using System;
using System.Net;
using StackExchange.Redis;

namespace Advertise.Core.Managers.Cache
{
    public interface IRedisConnectionWrapper : IDisposable
    {
        IDatabase GetDatabase(int? db = null);
        IServer GetServer(EndPoint endPoint);
        EndPoint[] GetEndPoints();
        void FlushDatabase(int? db = null);
        bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);
    }
}
