using System.ComponentModel;

namespace Advertise.Core.Types
{
    public enum RedirectionType
    {
        MultipleChoices = 300,
        MovedPermanently = 301,
        Found = 302,
        SeeOther = 303,
        NotModified = 304,
        UseProxy = 305,
        TemporaryRedirect = 307,
        PermanentRedirect = 308
    }
}