using System;
using Xunit;

namespace MeassureUnitTest
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvMeassureApi.Controllers;
    using EnvMeassureApi.Models;

    using Microsoft.AspNetCore.Mvc;

    public class UnitTest1
    {
        PersonalMeasurementController _controller = new PersonalMeasurementController();
        [Fact]
        public void GetWhenCalledReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetAll().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<PersonalMeasurement>>(okResult.Value);
        }
        //[Fact]
        //public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        //{
        //    // Act
        //    var notFoundResult = _controller.GetById(0);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(notFoundResult.Result);
        //}

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testid = "123";

            // Act
            var okResult = _controller.GetById(testid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testid = "123";

            // Act
            var okResult = _controller.GetById(testid).Result as OkObjectResult;

            // Assert
            Assert.IsType<List<PersonalMeasurement>>(okResult.Value);
            Assert.Equal(testid, (okResult.Value as List<PersonalMeasurement>)[0].UId.ToString());
        }
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var PressureMissingItem = new PersonalMeasurement();
                                      
            _controller.ModelState.AddModelError("Pressure", "Required");

            // Act
            var badResponse = _controller.Add(PressureMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            PersonalMeasurement testItem = new PersonalMeasurement(123,1000, 50, 20);

            // Act
            var createdResponse = _controller.Add(testItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new PersonalMeasurement(123, 1000, 50, 20);

            // Act
            var createdResponse = _controller.Add(testItem) as OkObjectResult;
            var item = createdResponse.Value as PersonalMeasurement;

            // Assert
            Assert.IsType<PersonalMeasurement>(item);
            Assert.Equal(testItem.Pressure, item.Pressure);
        }

        //[Fact]
        //public void Remove_ExistingGuidPassed_ReturnsOkResult()
        //{
        //    // Arrange
        //    var okResult = _controller.GetAll().Result as OkObjectResult;
        //    List<PersonalMeasurement> meassurements = okResult.Value as List<PersonalMeasurement>;
        //    int existingId = meassurements.Last().Id.GetValueOrDefault(0);

        //    // Act
        //    var okResponse = _controller.Delete(existingId) as OkObjectResult;
        //    int? rowsAffected = okResponse.Value as int?;
        //    // Assert
        //    Assert.Equal(1, rowsAffected.GetValueOrDefault(0));
        //}
        //[Fact]
        //public void GetById_SqlInjection_ReturnsBadRequest()
        //{
        //    // Arrange
        //    string sql = "1 OR 1=1";

        //    //Act
        //    var response = _controller.GetById(sql);

        //    // Assert
        //    Assert.IsType<BadRequestResult>(response.Result);
        //}

    }
}
