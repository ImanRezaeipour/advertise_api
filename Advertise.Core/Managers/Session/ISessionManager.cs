using System;

namespace Advertise.Core.Managers.Session
{
    public interface ISessionManager
    {
        string CreateSessionId();
        ISession GetSession(string strSesId);
        ISession RemoveSession(string strSesId);
    }

    public interface ISession
    {
        Object GetAttribute(string strKey);
        void SetAttribute(string strKey, Object objValue);
        Object RemoveAttribute(string strKey);
    }
}
