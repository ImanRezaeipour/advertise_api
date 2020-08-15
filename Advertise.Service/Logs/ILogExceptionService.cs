using System;

namespace Advertise.Service.Logs
{
    public interface ILogExceptionService
    {
        void Log(Exception exception);
        void Raise(Exception exception);
    }
}