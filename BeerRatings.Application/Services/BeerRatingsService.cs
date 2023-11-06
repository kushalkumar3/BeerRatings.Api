using BeerRatings.Application.Interfaces;
using BeerRatings.Core.Common;
using BeerRatings.Core.Domians;
using BeerRatings.Core.Entities;
using BeerRatings.Core.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerRatings.Application.Services
{
    public class BeerRatingsService : IBeerRatingsService
    {
        private readonly IPunkAPIWrapper _PunkAPIWrapper;
        private readonly IBeerRatingsRepository _BeerRatingsRepository;
        private readonly ILogger _logger;
        public BeerRatingsService(IPunkAPIWrapper PunkAPIWrapper, IBeerRatingsRepository BeerRatingsRepository
            ,ILogger logger)
        {
            _logger = logger;
            _PunkAPIWrapper = PunkAPIWrapper;
            _BeerRatingsRepository = BeerRatingsRepository;
        }

        public async Task<BeerRating> GetBeerRatingsByBeerName(string beerName)
        {
            try
            {
                _logger.Information("Geting beer details - {0} ", beerName);
                Beer beer = await _PunkAPIWrapper.GetBeerByName(beerName);
                _logger.Information("mapping beer details - {0} ", beerName);
                BeerRating beerRating = new BeerRating(beer);
                _logger.Information("beer ratings mapped! getting beer ratings - {0} ", beerName);
                beerRating.UserRatings = await _BeerRatingsRepository.GetBeerRatingsById(beer.Id);
                _logger.Information("beer ratings successfully retrieved - {0} ", beerName);
                return beerRating;
            }
            catch (Exception ex)
            {
                _logger.Error("Unable to get beer ratings! beerName: {0}, {1} ", beerName,ex.Message);
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Unable to get beer ratings! beerName:" + beerName);
            }
        }

        public async Task<bool> SaveBeerRatings(UserRating userRatings)
        {
            try
            {

                if (!await _PunkAPIWrapper.IsValidBeerId(userRatings.Id))
                {
                    _logger.Error("Invalid beer id: {0}", userRatings.Id);
                    throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Invalid beer id:" + userRatings.Id);
                }
                return await _BeerRatingsRepository.SaveBeerRating(userRatings);
            }
            catch (ApiException ex)
            {
                _logger.Error(ex.Message, ex.ReasonPhrase);
                throw ex;
            }
            catch
            {
                _logger.Error("Unable to save ratings! user name:{0}" + userRatings.UserName);
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Unable to save ratings! user name:" + userRatings.UserName);
            }
        }
    }
}
