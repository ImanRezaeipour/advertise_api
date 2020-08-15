using System;

namespace Advertise.Web.Utilities.Noty
{
    [Serializable]
    public class NotyMessage
    {
        public NotyAlertType Type { get; set; }

        public string Message { get; set; }

        public NotyAnimationType CloseAnimation { get; set; }

        public NotyAnimationType OpenAnimation { get; set; }

        public int AnimateSpeed { get; set; }

        public bool IsSticky { get; set; }

        public NotyMessageCloseType CloseWith { get; set; }

        public NotyMessageLocationType Location { get; set; }

        public bool IsSwing { get; set; }

        public bool IsKiller { get; set; }

        public bool IsForce { get; set; }

        public bool IsModal { get; set; }
    }
}