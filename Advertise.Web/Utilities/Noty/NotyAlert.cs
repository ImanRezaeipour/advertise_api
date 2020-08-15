using System;
using System.Collections.Generic;

namespace Advertise.Web.Utilities.Noty
{
    [Serializable]
    public class NotyAlert
    {
        public const string TempDataKey = "TempDataNotyAlerts";

        private IList<NotyMessage> _notyMessages;

        public NotyAlert()
        {
            NotyMessages = new List<NotyMessage>();
            MaxVisibleForQueue = 20;
        }

        public bool DismissQueue { get; set; }

        public int MaxVisibleForQueue { get; set; }

        public IList<NotyMessage> NotyMessages
        {
            get { return _notyMessages; }
            set { _notyMessages = value; }
        }

        public NotyMessage AddNotyMessage(NotyMessage message)
        {
            NotyMessages.Add(message);
            return message;
        }
    }
}