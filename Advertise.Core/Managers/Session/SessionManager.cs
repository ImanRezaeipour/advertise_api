using System;
using System.Collections;
using System.Threading;

namespace Advertise.Core.Managers.Session
{
    public class SessionManager : ISessionManager
    {
        Hashtable hsSession = new Hashtable();
        static SessionManager objSM;
       
        private static Timer tmrSessionTimeout;
      
        private SessionManager()
        {
        }
        
        public static SessionManager getInstance()
        {
            if (objSM == null)
            {
                objSM = new SessionManager();
                //create a new objectof the timer class and associte //a timer call back delegate. The removesession
                //method will be invoked by the timer object for //every 1000 milliseconds (change it as u like).
                tmrSessionTimeout = new Timer(new TimerCallback(objSM.removesession), null, 0, 100);
                return objSM;
            }
            return objSM;
        }
    
        private void removesession(object state)
        {
            IDictionaryEnumerator objSesEnum = hsSession.GetEnumerator();
            //A temporary session object.
            ISession tmpSesObj;
            ArrayList arrExpiredKeys = new ArrayList();
            //for each session, check the last used datetime.
            while (objSesEnum.MoveNext())
            {
                //The session will be removed, if the user is idle for 2 hours. (change it as u like).
                tmpSesObj = (ISession)objSesEnum.Value;
                DateTime dt = DateTime.Parse(tmpSesObj.GetAttribute("lastAccessedDatetime").ToString());
                TimeSpan ts = dt.Subtract(DateTime.Now);
                //If the difference between the last accessed //datetime and current date time is equal to 2 hours //then the session id will be added to the arraylist.
                //(ie., left idle for 2 hours)
                if (dt.Subtract(DateTime.Now).Hours >= 2)
                {
                    arrExpiredKeys.Add(objSesEnum.Key);
                }
                //remove the list of keys added to the array list.
                int i = 0;
                while (arrExpiredKeys.Count > i)
                {
                    //remove the session from the hashtable.
                    hsSession.Remove(arrExpiredKeys[i]);
                }
            }
        }
        
        public string CreateSessionId()
        {
            //Create a new guid which will act as the session id
            string newSessionId = Guid.NewGuid().ToString();
            //create a new session object and store the object in the //hashtable against the new session id (GUID).
            Session objS = new Session();
            hsSession.Add(newSessionId, objS);
            //Set the session id into the session object.
            objS.SetAttribute("SessionId", newSessionId);
            //return the session id, 
            return newSessionId;
        }
        
        public ISession GetSession(string strSesId)
        {
            ISession objS = (ISession)hsSession[strSesId];
            //everytime when the session object is retried, update the //lastaccesseddate variable with the latest datetime
            objS.SetAttribute("lastAccessedDatetime", DateTime.Now);
            return objS; //return the session object
        }
        
        public ISession RemoveSession(string strSesId)
        {
            ISession objS = (ISession)hsSession[strSesId];
            //remove the sessionid from the hash table.
            hsSession.Remove(strSesId);
            return objS; //return the removed session
        }
    }
}
