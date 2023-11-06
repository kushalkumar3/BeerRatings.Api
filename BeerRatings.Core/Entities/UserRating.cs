

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BeerRatings.Core.Entities
{
    public class UserRating
    {
        [JsonIgnore]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("comments")]
        public string Comments { get; set; }
    }
   
   
}
