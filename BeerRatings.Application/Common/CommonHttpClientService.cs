using BeerRatings.Application.Interfaces;
using BeerRatings.Core.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BeerRatings.Application.Common
{
    public class CommonHttpClientService : ICommonHttpClientService
    {
        
        private readonly HttpClient _httpClient;

        public CommonHttpClientService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.punkapi.com"); 
        }

        public async Task<string> MakeGetRequestAsync(string endPoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/v2/beers" + endPoint); 

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Invalid beerid!");                    
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Unable to communicate with punk api! end point:" + endPoint);
            }
        }
    }

}
