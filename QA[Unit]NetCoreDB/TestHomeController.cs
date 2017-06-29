using ReactNetCoreDB.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReactNetCoreDB.Business_logic;
using System.Threading.Tasks;

namespace QANetCoreDB
{
    [TestClass]
    public class TestHomeController
    {
        private Mock<IQuery> query = null;
        private HomeController testHomeController = null;

        [TestInitialize]
        public void SetupContext() 
        {
            //Initialize
            query = new Mock<IQuery>();
            testHomeController = new HomeController(query.Object);
        }

        [TestMethod]
        public async Task TestHomeControllerBikeDetails()
        {
            //Act
            var actual = await testHomeController.BikeDetails(1);
            //Assert
            query.Verify(Details => Details.BikeDetails(1), Times.Once());
        }

        [TestMethod]
        public async Task TestHomeControllerFindBikes()
        {
            //Act
            var actual = await testHomeController.FindBikes(0, "1");
            //Assert
            query.Verify(bikes => bikes.FindBikes("1", 0), Times.Once());
        }

        [TestMethod]
        public async Task TestHomeControllerTopBikes()
        {
            //Act
            var actual = await testHomeController.TopBikes();
            //Assert
            query.Verify(top => top.TopBikes(), Times.Once());
        }
    }
}
