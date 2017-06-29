using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReactNetCoreDB.Business_logic;
using System.Linq;
using ReactNetCoreDB.Models;

namespace QANetCoreDB
{
    [TestClass]
    public class TestDataAccessLayer
    {
        private Mock<IDataAccessLayer> data = null;
        private Query testQuery = null;

        [TestInitialize]
        public void SetupContext()
        {
            //Initialize
            data = new Mock<IDataAccessLayer>();
            testQuery = new Query(data.Object);
        }

        [TestMethod]
        public void TestDataAccessLayerGetAllBikes()
        {
            //Act
            var actual = testQuery.FindBikes("1",1);
            //Assert
            data.Verify(bikes => bikes.GetAllBikes(), Times.Once());
        }

        [TestMethod]
        public void TestDataAccessLayerGetAllBikesDetails()
        {
            //Act
            var actual = testQuery.BikeDetails(0);
            //Assert
            data.Verify(bikes => bikes.GetAllBikesDetails(), Times.Once());
        }

        [TestMethod]
        public void TestDataAccessLayerGetTopBikes()
        {
            //Act
            var actual = testQuery.TopBikes();
            //Assert
            data.Verify(bikes => bikes.GetTopBikes(), Times.Once());
        }
    }
}
