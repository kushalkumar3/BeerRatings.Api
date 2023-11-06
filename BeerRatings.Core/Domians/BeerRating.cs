using BeerRatings.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeerRatings.Core.Domians
{
    public class BeerRating
    {
        public BeerRating(Beer beer) {
            this.Id = beer.Id;
            this.Name = beer.Name;
            this.Description = beer.Description;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("userRatings")]
        public List<UserRating> UserRatings { get; set; }
    }
}
