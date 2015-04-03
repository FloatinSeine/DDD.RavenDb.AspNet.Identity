using System;
using System.Security.Claims;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Sample.Domain.Persistence.Raven.Convertors
{
    public class ClaimJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Claim));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            string type = (string)jo["Type"];
            string value = (string)jo["Value"];
            string valueType = (string)jo["ValueType"];
            string issuer = (string)jo["Issuer"];
            string originalIssuer = (string)jo["OriginalIssuer"];
            return new Claim(type, value, valueType, issuer, originalIssuer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (null == value)
            {
                writer.WriteNull();
                return;
            }

            var claim = value as Claim;
            if (claim != null)
            {
                writer.WritePropertyName("Type");
                writer.WriteValue(claim.Type);
                writer.WritePropertyName("Value");
                writer.WriteValue(claim.Value);
                writer.WritePropertyName("ValueType");
                writer.WriteValue(claim.ValueType);
                writer.WritePropertyName("Issuer");
                writer.WriteValue(claim.Issuer);
                writer.WritePropertyName("OriginalIssuer");
                writer.WriteValue(claim.OriginalIssuer);
                writer.WritePropertyName("Subject");
                writer.WriteValue(claim.Subject);
                return;
            }
            var msg = string.Format("Unable to serialize {0} with {1}", value.GetType(), typeof(ClaimJsonConverter));
            throw new InvalidOperationException(msg);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}
