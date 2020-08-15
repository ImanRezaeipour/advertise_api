using System.Linq;

namespace Advertise.Service.Common
{
    public static class JsonExtensions
    {
        public static string StringArrayToJson(this string[] array)
        {
            if (array == null || !array.Any())
                return "[]";
            else
                return Newtonsoft.Json.JsonConvert.SerializeObject(array);
        }
    }
}
