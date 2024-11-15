using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TD1.Controllers;
using TD1.Models.DataManager;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TestTD1.Controller
{
    [TestClass()]
    public class MarqueControllerTests
    {
        private ProduitDbContext context;

        private IDataRepository<Marque> dataRepository;

        public MarqueControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ProduitDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TD1; uid =postgres; password =postgres; ");
            context = new ProduitDbContext(builder.Options);
            dataRepository = new MarqueManager(context);
            dataRepository = new MarqueManager(context);
        }


        [TestMethod]
        public void GetMarqueById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Marque marque = new Marque
            {
                Idmarque = 1,
                Nommarque = "Calida"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(marque);

            var marqueController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueController.GetMarque(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(marque, actionResult.Value as Marque);
        }

        [TestMethod]
        public void GetMarqueById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marqueController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueController.GetMarque(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Postmarque_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marqueController = new MarquesController(mockRepository.Object);

            Marque marque = new Marque
            {
                Nommarque = "BBBBB"
            };

            // Act
            var actionResult = marqueController.PostMarque(marque.Nommarque).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Marque>), "Pas un ActionResult<Marque>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Marque), "Pas une Marque");
            marque.Idmarque = ((Marque)result.Value).Idmarque;
            Assert.AreEqual(marque, (Marque)result.Value, "Marques pas identiques");
        }

        [TestMethod]
        public void DeleteMarqueTest_AvecMoq()
        {
            // Arrange
            Marque marque = new Marque
            {
                Idmarque = 1,
                Nommarque = "hhhh"
            };

            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(marque);
            var marqueController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueController.DeleteMarque(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void Putmarque_ModelValidated_UpdateOK_AvecMoq()
        {
            // Arrange
            Marque marqueAMaJ = new Marque
            {
                Idmarque = 1,
                Nommarque = "jjjjjj"
            };
            Marque marqueUpdated = new Marque
            {
                Idmarque = 1,
                Nommarque = "hhhhhh"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(marqueAMaJ);
            var marqueController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueController.PutMarque(marqueUpdated.Idmarque, marqueUpdated.Nommarque).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}