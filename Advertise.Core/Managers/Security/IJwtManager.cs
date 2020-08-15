namespace Advertise.Core.Managers.Security
{
    public interface IJwtManager
    {
        string Encode (object obj);
        TModel DecodeToObject<TModel>(string encode) where TModel: class ;
        string Decode(string encode);
    }
}