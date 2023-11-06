using BeerRatings.Api.Controllers;
using BeerRatings.Core.Common;
using BeerRatings.Core.Domians;
using BeerRatings.Core.Entities;
using BeerRatings.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BeerRatings.Api.Tests
{
	[TestClass]
	public class BeerRatingsControllerTests
	{
        [TestMethod]
        public async Task Post_ValidData_ReturnsOk()
        {
            // Arrange
            int id = 123;
            var userRatings = new UserRating { Rating = 4 };
            var beerRatingsServiceMock = new Mock<IBeerRatingsService>();
            var loggerMock = new Mock<ILogger>();
            var controller = new BeerRatingsController(beerRatingsServiceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.Post(userRatings, id);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<string>));
        }

        [TestMethod]
        public async Task Post_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = -1; // Invalid ID
            var userRatings = new UserRating { Rating = 4 };
            var beerRatingsServiceMock = new Mock<IBeerRatingsService>();
            var loggerMock = new Mock<ILogger>();
            var controller = new BeerRatingsController(beerRatingsServiceMock.Object, loggerMock.Object);

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<ApiException>(() => controller.Post(userRatings, id));           
            
            Assert.AreEqual("Invalid Id", exception.Message);
        }

        [TestMethod]
        public async Task Post_InvalidRating_ReturnsBadRequest()
        {
            // Arrange
            int id = 123;
            var userRatings = new UserRating { Rating = 6 }; // Invalid rating
            var beerRatingsServiceMock = new Mock<IBeerRatingsService>();
            var loggerMock = new Mock<ILogger>();
            var controller = new BeerRatingsController(beerRatingsServiceMock.Object, loggerMock.Object);

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<ApiException>(() => controller.Post(userRatings, id));           
            
            Assert.AreEqual("Invalid Rating! Please enter a valid number between 1 to 5 and No decimals allowed.", exception.Message);
        }
        [TestMethod]
        public async Task Get_ReturnsOkWithBeerRatings()
        {
            // Arrange
            string beerName = "SampleBeer";
            var beerRatingsServiceMock = new Mock<IBeerRatingsService>();
            var result1 = new BeerRating(new Beer());
            beerRatingsServiceMock.Setup(service => service.GetBeerRatingsByBeerName(beerName)).ReturnsAsync(result1);

            var loggerMock = new Mock<ILogger>();
            var controller = new BeerRatingsController(beerRatingsServiceMock.Object, loggerMock.Object);

            // Act
            IHttpActionResult result = await controller.Get(beerName);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<BeerRating>));
        }

        [TestMethod]
        public async Task Get_InvalidBeerName_ReturnsBadRequest()
        {
            // Arrange
            string beerName = null; // Invalid beer name
            var beerRatingsServiceMock = new Mock<IBeerRatingsService>();
            var loggerMock = new Mock<ILogger>();
            beerRatingsServiceMock.Setup(service => service.GetBeerRatingsByBeerName(beerName)).Throws(new ApiException(System.Net.HttpStatusCode.BadRequest, "Bad Request", "Unable to get beer ratings! beerName:" + beerName));
            var controller = new BeerRatingsController(beerRatingsServiceMock.Object, loggerMock.Object);

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ApiException>(() => controller.Get(beerName));

            // Assert
            Assert.AreEqual("Unable to get beer ratings! beerName:", exception.Message);
        }
    }
}
