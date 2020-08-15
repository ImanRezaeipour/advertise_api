using System.Xml;

namespace Advertise.Core.Extensions
{
    public static class XmlExtension
    {
        #region Public Methods

        public static string ElText(this XmlNode node, string elName)
        {
            return node.SelectSingleNode(elName)?.InnerText;
        }

        #endregion Public Methods
    }
}