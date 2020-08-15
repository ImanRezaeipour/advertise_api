namespace Advertise.Web.Utilities.HiddenField
{
    public interface IActionKeyService
    {
        string GetActionKey(string token, string controller, string area = "");
    }
}