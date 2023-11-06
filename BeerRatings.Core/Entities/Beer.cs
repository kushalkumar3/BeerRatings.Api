using Newtonsoft.Json;
using System.Text.Json;

namespace BeerRatings.Core.Entities
{
    public class Beer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
       
    }
}
