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
    public class TypeProduitsControllerTests
    {
        private ProduitDbContext context;

        private IDataRepository<TypeProduit> dataRepository;

        public TypeProduitsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ProduitDbContext>()
                .UseNpgsql("Server=localhost;port=5432;Database=TD1; uid=postgres; password=postgres;");
            context = new ProduitDbContext(builder.Options);
            dataRepository = new TypeproduitManager(context);
        }

        [TestMethod]
        public void GetTypeProduitById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeProduit typeProduit = new TypeProduit
            {
                Idtypeproduit = 1,
                Nomtypeproduit = "ProduitTest"
            };
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(typeProduit);

            var typeProduitsController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitsController.GetTypeProduit(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(typeProduit, actionResult.Value as TypeProduit);
        }

        [TestMethod]
        public void GetTypeProduitById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            var typeProduitsController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitsController.GetTypeProduit(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostTypeProduit_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            var typeProduitsController = new TypeProduitsController(mockRepository.Object);

            TypeProduit typeProduit = new TypeProduit
            {
                Nomtypeproduit = "ProduitTest"
            };

            // Act
            var actionResult = typeProduitsController.PostTypeProduit(typeProduit.Nomtypeproduit).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeProduit>), "Pas un ActionResult<TypeProduit>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeProduit), "Pas un TypeProduit");
            typeProduit.Idtypeproduit = ((TypeProduit)result.Value).Idtypeproduit;
            Assert.AreEqual(typeProduit, (TypeProduit)result.Value, "TypeProduits pas identiques");
        }

        [TestMethod]
        public void DeleteTypeProduitTest_AvecMoq()
        {
            // Arrange
            TypeProduit typeProduit = new TypeProduit
            {
                Idtypeproduit = 1,
                Nomtypeproduit = "ProduitTest"
            };

            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(typeProduit);
            var typeProduitsController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitsController.DeleteTypeProduit(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public void PutTypeProduit_ModelValidated_UpdateOK_AvecMoq()
        {
            // Arrange
            TypeProduit typeProduitAMaJ = new TypeProduit
            {
                Idtypeproduit = 1,
                Nomtypeproduit = "ProduitOriginal"
            };
            TypeProduit typeProduitUpdated = new TypeProduit
            {
                Idtypeproduit = 1,
                Nomtypeproduit = "ProduitMisAJour"
            };
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(typeProduitAMaJ);
            var typeProduitsController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitsController.PutTypeProduit(typeProduitUpdated.Idtypeproduit, typeProduitUpdated.Nomtypeproduit).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}
