using System.Collections.Generic;
using System.Linq;

namespace Advertise.Web.Utilities.HiddenField
{
    public class ActionKeyService : IActionKeyService
    {
        private static readonly IList<ActionKey> ActionKeys;

        static ActionKeyService()
        {
            ActionKeys = new List<ActionKey>();
        }

        public string GetActionKey(string token, string controller, string area = "")
        {
            area = area ?? "";
            var actionKey = ActionKeys.FirstOrDefault(a =>
                a.Controller.ToLower() == controller.ToLower() &&
                a.ActionKeyValue == token &&
                a.Area.ToLower() == area.ToLower());
            return actionKey != null ? actionKey.ActionKeyValue : AddActionKey(token, controller, area);
        }

        private string AddActionKey(string token, string controller, string area = "")
        {
            var actionKey = new ActionKey
            {
                Controller = controller,
                Area = area,
                ActionKeyValue = token
            };
            ActionKeys.Add(actionKey);
            return actionKey.ActionKeyValue;
        }
    }
}