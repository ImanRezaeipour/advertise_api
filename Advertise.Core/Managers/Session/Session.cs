using System;
using System.Collections;

namespace Advertise.Core.Managers.Session
{
    public class Session : ISession
    {
        Hashtable hsValues = new Hashtable();

        public Session()
        {
        }
        
        public object GetAttribute(string strKey)
        {
            //get the value for the key specified
            return hsValues[strKey];
        }
        
        public void SetAttribute(string strKey, Object objValue)
        {
            //add a new value, if the key already exists then it will //be overridden
            hsValues.Add(strKey, objValue);
        }
        
        public object RemoveAttribute(string strKey)
        {
            object obj = hsValues[strKey]; //fetch the value
            hsValues.Remove(strKey);
            return obj; //return the value that is removed
        }
    }
}
