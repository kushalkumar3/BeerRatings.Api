using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;


namespace BeerRatings.Core.Common
{
    public class IgnoreJsonAttributesResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            foreach (var prop in props)
            {
                prop.Ignored = false;   // Ignore [JsonIgnore]
                prop.Converter = null;  // Ignore [JsonConverter]
                prop.PropertyName = prop.UnderlyingName;  // restore original property name
            }
            return props;
        }
    }
}
