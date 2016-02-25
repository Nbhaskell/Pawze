using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pawze.API;
using Pawze.API.Controllers;
using Pawze.Core.Domain;
using Pawze.Core.Infrastructure;
using Pawze.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pawze.Test.Controllers
{
    [TestClass]
    public class BoxesControllerTests
    {
        private Mock<IBoxRepository> _boxRepository = null;
        private Mock<IUnitOfWork> _unitOfWork = null;
        private Mock<IPawzeUserRepository> _pawzeUserRepository = null;

        BoxesController controller = null;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            WebApiConfig.CreateMaps();

            _boxRepository = new Mock<IBoxRepository>();
            _boxRepository.Setup(b => b.GetWhere(It.IsAny<Expression<Func<Box, bool>>>())).Returns(new List<Box>
            {
                new Box { BoxId = 1, PawzeUserId = "test1", SubscriptionId = 1 },
                new Box { BoxId = 2, PawzeUserId = "test2", SubscriptionId = 2 }
            });

            _unitOfWork = new Mock<IUnitOfWork>();
            _pawzeUserRepository = new Mock<IPawzeUserRepository>();

            controller = new BoxesController(_boxRepository.Object, _unitOfWork.Object, _pawzeUserRepository.Object);
        }

        [TestMethod]
        public void GetAllShouldReturnAll()
        {
            // Act
            var result = controller.GetBoxes();

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void GetBoxByIdShouldReturnSingleBox()
        {
            // Act
        }

        [TestMethod]
        public void GetBoxItemsForBoxShouldReturnBoxItems()
        {
            // Arrange

            // Act
            var boxItems = controller.GetBoxItemsForBox(5);

            // Assert 
            Assert.IsTrue(boxItems.Count() > 0);
        }
    }
}
