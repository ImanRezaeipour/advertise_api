using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace Advertise.Core.Managers.Security
{
    public class JwtManager : IJwtManager
    {
        public string Encode (object obj)
        {
            var payload =  obj;
            var secret = "Secret";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }

        public TModel DecodeToObject<TModel>(string encode) where TModel : class 
        {
            var token = encode;
            var secret = "Secret";
           
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                //var json = decoder.Decode(token, secret, verify: true);

            var payload = decoder.DecodeToObject<TModel>(token, secret,true);
            //catch (TokenExpiredException)
            //{
            //    Console.WriteLine("Token has expired");
            //}
            //catch (SignatureVerificationException)
            //{
            //    Console.WriteLine("Token has invalid signature");
            //}
            return payload;
        }

        public string Decode(string encode) 
        {
            var token = encode;
            var secret = "Secret";

            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var payload = decoder.Decode(token, secret, verify: true);

            return payload;
        }
    }
}
