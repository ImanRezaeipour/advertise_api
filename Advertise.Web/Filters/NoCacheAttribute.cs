using System;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class NoCacheAttribute : OutputCacheAttribute
    {
        public NoCacheAttribute()
        {
            // Duration = 0 by default.
            // VaryByParam = "*" by default.
            this.NoStore = true;
        }
    }
}
