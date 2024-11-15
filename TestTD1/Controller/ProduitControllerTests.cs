using Microsoft.AspNetCore.Mvc;
using Moq;
using TD1.Controllers;
using TD1.Models.DataManager;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TD1.Models.Simple;

namespace TestTD1.Controller
{
    [TestClass]
    public class ProduitsControllerTests
    {
        private Mock<ProduitManager> _mockRepository;
        private ProduitsController _produitsController;
        private ProduitDbContext context;
        private IDataRepository<Produit> dataRepository;

        [TestInitialize]
        public void setup()
        {
            _mockRepository = new Mock<ProduitManager>();
            _produitsController = new ProduitsController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetProduitById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            var produit = new Produit { Idproduit = 1, Nomproduit = "Produit1" };
            // Arrange

            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(new ActionResult<Produit>(produit));

            // Act
            var actionResult = await _produitsController.GetProduit(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(produit, actionResult.Value as Produit);
        }

        [TestMethod]
        public async Task GetProduitById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {

            // Act
            var actionResult = await _produitsController.GetProduit(0);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostProduit_ModelValidated_CreationOK_AvecMoq()
        {
            var mockRepositoryMarque = new Mock<IDataRepository<Marque>>();
            var marquesController = new MarquesController(mockRepositoryMarque.Object);  // Assurez-vous d'avoir ce contrôleur pour gérer les marques

            var mockRepositoryTypeProduit = new Mock<IDataRepository<TypeProduit>>();
            var typesProduitsController = new TypeProduitsController(mockRepositoryTypeProduit.Object);  // Contrôleur pour les types de produits

            // 1. Créer une nouvelle marque via POST
            Marque nouvelleMarque = new Marque
            {
                Nommarque = "MarqueTest"
            };

            var actionResultMarque = await marquesController.PostMarque(nouvelleMarque.Nommarque);
            var resultMarque = actionResultMarque.Result as CreatedAtActionResult;
            var marqueCree = resultMarque.Value as Marque;
            var test = await marquesController.GetMarque(marqueCree.Idmarque);

            // 2. Créer un nouveau type de produit via POST
            TypeProduit nouveauTypeProduit = new TypeProduit
            {
                Nomtypeproduit = "TypeProduitTest"
            };

            var actionResultTypeProduit = await typesProduitsController.PostTypeProduit(nouveauTypeProduit.Nomtypeproduit);
            var resultTypeProduit = actionResultTypeProduit.Result as CreatedAtActionResult;
            var typeProduitCree = resultTypeProduit.Value as TypeProduit;

            // 3. Créer un produit avec la référence à la marque et au type de produit créés
            ProduitSimple produit = new ProduitSimple
            {
                NomProduit = "NouveauProduit",
                Description = "Description du nouveau produit",
                Nomphoto = "photo.jpg",
                Uriphoto = "http://example.com/photo.jpg",
                Stockreel = 50,
                Stockmin = 5,
                Stockmax = 100,
                IdMarque = marqueCree.Idmarque,  // Référence à la marque créée
                IdTypeproduit = typeProduitCree.Idtypeproduit  // Référence au type de produit créé
            };

            // Act
            var actionResult = await _produitsController.PostProduit(produit);

            // Assert
            var result = actionResult as CreatedAtActionResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(Produit));
            /*            produit.Idproduit = ((Produit)result.Value).Idproduit;*/
            Produit produitEqual = new Produit
            {
                Nomproduit = produit.NomProduit,
                Description = produit.Description,
                Nomphoto = produit.Nomphoto,
                Uriphoto = produit.Uriphoto,
                Stockreel = produit.Stockreel,
                Stockmin = produit.Stockmin,
                Stockmax = produit.Stockmax,
                Idmarque = marqueCree.Idmarque,  // Référence à la marque créée
                Idtypeproduit = typeProduitCree.Idtypeproduit  // Référence au type de produit créé
            };

            Assert.AreEqual(produitEqual, result.Value);
        }

        [TestMethod]
        public async Task DeleteProduitTest_AvecMoq()
        {
            // Arrange
            Produit produit = new Produit
            {
                Idproduit = 1,
                Nomproduit = "ProduitASupprimer",
                Description = "Produit à supprimer",
                Nomphoto = "photo.jpg",
                Uriphoto = "http://example.com/photo.jpg",
                Stockreel = 100,
                Stockmin = 10,
                Stockmax = 200,
                Idmarque = 1,
                Idtypeproduit = 1
            };

            _mockRepository.Setup(x => x.GetById(1).Result).Returns(produit);

            // Act
            var actionResult = await _produitsController.DeleteProduit(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutProduit_ModelValidated_UpdateOK_AvecMoq()
        {
            // Arrange
            Produit produitAMaJ = new Produit
            {
                Idproduit = 1,
                Nomproduit = "ProduitAUpdate",
                Description = "Description avant update",
                Nomphoto = "photo1.jpg",
                Uriphoto = "http://example.com/photo1.jpg",
                Stockreel = 100,
                Stockmin = 10,
                Stockmax = 200,
                Idmarque = 1,
                Idtypeproduit = 1
            };

            ProduitSimple produitUpdated = new ProduitSimple
            {
                Idproduit = 1,
                NomProduit = "ProduitUpdated",
                Description = "Description après update",
                Nomphoto = "photoUpdated.jpg",
                Uriphoto = "http://example.com/photoUpdated.jpg",
                Stockreel = 80,
                Stockmin = 8,
                Stockmax = 160
            };

            _mockRepository.Setup(x => x.GetById(1).Result).Returns(produitAMaJ);

            // Act
            var actionResult = await _produitsController.PutProduit(produitUpdated.Idproduit, produitUpdated);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}
