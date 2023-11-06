using BeerRatings.Application.Interfaces;
using BeerRatings.Application.Services;
using BeerRatings.Core.Common;
using BeerRatings.Core.Entities;
using BeerRatings.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerRatings.Application.Tests.Services
{

    [TestClass]
    public class BeerRatingsServiceTests
    {
        [TestMethod]
        public async Task GetBeerRatingsByBeerName_ValidBeerName_ReturnsBeerRating()
        {
            // Arrange
            var beerName = "TestBeer";
            var beer = new Beer { Id = 1, Description = "", Name = beerName };
            var userRatings = new List<UserRating> { new UserRating() { Id = 1, Rating = 1, UserName = "test@test.com" } };

            var punkAPIWrapperMock = new Mock<IPunkAPIWrapper>();
            punkAPIWrapperMock.Setup(mock => mock.GetBeerByName(beerName)).ReturnsAsync(beer);

            var beerRatingsRepositoryMock = new Mock<IBeerRatingsRepository>();
            beerRatingsRepositoryMock.Setup(mock => mock.GetBeerRatingsById(beer.Id)).ReturnsAsync(userRatings);

            var loggerMock = new Mock<ILogger>();

            var beerRatingsService = new BeerRatingsService(punkAPIWrapperMock.Object, beerRatingsRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await beerRatingsService.GetBeerRatingsByBeerName(beerName);

            // Assert
            Assert.IsNotNull(result);
            result.Name.Equals(beerName);
        }

        [TestMethod]
        public async Task SaveBeerRatings_ValidUserRatings_ReturnsTrue()
        {
            // Arrange
            var userRatings = new UserRating { Id = 1, Rating = 1, UserName = "test@test.com" };

            var punkAPIWrapperMock = new Mock<IPunkAPIWrapper>();
            punkAPIWrapperMock.Setup(mock => mock.IsValidBeerId(userRatings.Id)).ReturnsAsync(true);

            var beerRatingsRepositoryMock = new Mock<IBeerRatingsRepository>();
            beerRatingsRepositoryMock.Setup(mock => mock.SaveBeerRating(userRatings)).ReturnsAsync(true);

            var loggerMock = new Mock<ILogger>();

            var beerRatingsService = new BeerRatingsService(punkAPIWrapperMock.Object, beerRatingsRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await beerRatingsService.SaveBeerRatings(userRatings);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task SaveBeerRatings_InvalidUserRatings_ThrowsApiException()
        {
            // Arrange
            var userRatings = new UserRating { Id = 1 };

            var punkAPIWrapperMock = new Mock<IPunkAPIWrapper>();
            punkAPIWrapperMock.Setup(mock => mock.IsValidBeerId(userRatings.Id)).ReturnsAsync(false);

            var beerRatingsRepositoryMock = new Mock<IBeerRatingsRepository>();
            var loggerMock = new Mock<ILogger>();

            var beerRatingsService = new BeerRatingsService(punkAPIWrapperMock.Object, beerRatingsRepositoryMock.Object, loggerMock.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ApiException>(async () => await beerRatingsService.SaveBeerRatings(userRatings));
        }
    }

}

