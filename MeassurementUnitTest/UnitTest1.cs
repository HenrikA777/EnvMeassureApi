using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeassurementUnitTest
{
    using System.Collections.Generic;

    using EnvMeassureApi.Controllers;

    [TestClass]
    public class UnitTest1
    {
        MeassurementController controller = new MeassurementController();
        [TestMethod]
        public void GetAllTest()
        {
            
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
    }
}
