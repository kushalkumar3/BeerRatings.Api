using BeerRatings.Application.Interfaces;
using BeerRatings.Core.Entities;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BeerRatings.Core.Common;

namespace BeerRatings.Application.Wrapper
{
    public class PunkAPIWrapper : IPunkAPIWrapper
    {
        private readonly ICommonHttpClientService _httpClient;
        public PunkAPIWrapper(ICommonHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Beer> GetBeerByName(string beerName)
        {
            try
            {
                string jsonBeerDetails = await _httpClient.MakeGetRequestAsync(string.Format("?beer_name={0}", beerName));
                var item = JsonConvert.DeserializeObject<dynamic>(jsonBeerDetails);
                Beer beer = new Beer();
                beer.Id = item[0].id;
                beer.Name = item[0].name;
                beer.Description = item[0].description;
                return beer;

            }
            catch (Exception ex)
            {
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Unable to map beer properties!");
            }
        }

        public async Task<bool> IsValidBeerId(int beerID)
        {
            string jsonResult = await _httpClient.MakeGetRequestAsync(string.Format("/{0}", beerID));
            return (jsonResult.IndexOf("id") > -1) ? true : false;
        }

    }
}
