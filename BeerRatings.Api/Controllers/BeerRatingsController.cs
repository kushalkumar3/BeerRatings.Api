using BeerRatings.Core.Entities;
using BeerRatings.Core.Domians;
using System.Web.Http;
using BeerRatings.Core.Interfaces;
using System.Threading.Tasks;
using BeerRatings.Api.Filters;
using System.Net;
using System.Text.RegularExpressions;
using BeerRatings.Core.Common;
using Serilog;

namespace BeerRatings.Api.Controllers
{
    [ApiExceptionFilter]
    public class BeerRatingsController : ApiController
    {
        private readonly IBeerRatingsService _beerRatingsService;
        private readonly ILogger _logger;
        public BeerRatingsController(IBeerRatingsService beerRatingsService, ILogger logger) {
            _beerRatingsService = beerRatingsService;
            _logger = logger;
        }
        // GET api/<controller>/beerName
        public async Task<IHttpActionResult> Get(string beerName)
        {
            _logger.Information("Getting beer ratings for beer name:{0}",beerName);
            return Ok(await _beerRatingsService.GetBeerRatingsByBeerName(beerName));
        }

        // POST api/<controller>/Id
        [UserNameValidationAttribute]
        public async Task<IHttpActionResult> Post([FromBody] UserRating userRatings,[FromUri] int id)
        {
            if (id <= 0)
            {
                _logger.Error("Invalid beer id:{0}", id);
                throw new ApiException(HttpStatusCode.BadRequest, "Bad Request", "Invalid Id");
            }
            if (userRatings.Rating <= 0 || userRatings.Rating > 5 )
            {
                _logger.Error("Invalid Rating:{0}", userRatings.Rating);
                throw new ApiException(HttpStatusCode.BadRequest, "Bad Request", "Invalid Rating! Please enter a valid number between 1 to 5 and No decimals allowed.");
            }
            _logger.Information("Saving beer ratings - {0}", id);
            userRatings.Id = id;
            await _beerRatingsService.SaveBeerRatings(userRatings);
            _logger.Information("beer ratings saved - {0}", id);
            return Ok("Rating successfully added!");
        }
    }
}