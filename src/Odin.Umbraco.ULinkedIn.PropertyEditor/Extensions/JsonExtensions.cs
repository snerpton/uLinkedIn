using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Odin.Umbraco.ULinkedIn.PropertyEditor.Extensions
{
    internal static class JsonExtensions
    {
        internal static string SerializeToJson(this object obj,
            IEnumerable<JavaScriptConverter> converters = null)
        {
            var serializer = new JavaScriptSerializer();

            if (converters != null)
                serializer.RegisterConverters(converters);

            return serializer.Serialize(obj);
        }

        internal static TEntity DeserializeJsonTo<TEntity>(this string json,
            IEnumerable<JavaScriptConverter> converters = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();

                if(converters != null)
                    serializer.RegisterConverters(converters);

                return serializer.Deserialize<TEntity>(json);
            }
            catch (ArgumentException)
            {
                return default(TEntity);
            }
        }
    }
}
