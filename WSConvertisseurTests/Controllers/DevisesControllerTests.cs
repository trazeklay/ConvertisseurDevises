using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DevisesController controller = new DevisesController(); // Arrange
            var result = controller.GetAll().ToList(); // Act
            List<Devise> devises = new List<Devise>() // Initialiser la valeur de comparaison
            {
                new Devise(1, "Dollar", 1.08),
                new Devise(2, "Franc Suisse", 1.07),
                new Devise(3, "Yen", 120),
            };

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(List<Devise>), "Pas une liste de devises"); // Test du type de retour
            Assert.AreEqual(result, devises, "Erreur lors du chargement des listes"); // Test du contenu du retour
            /**/
        }

        [TestMethod()]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            DevisesController controller = new DevisesController(); // Arrange
            var result = controller.GetById(1); // Act

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); // Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); // Test de la devise récupérée
            /**/
        }

        [TestMethod()]
        public void GetById_UnknownIdPassed_Returns404Error()
        {
            DevisesController controller = new DevisesController(); // Arrange
            var result = controller.GetById(50); // Act

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Doit être un NotFoundResult"); // Test du type de l'erreur
            Assert.IsNotNull(result.Result, "Erreur est nul"); // Test de l'erreur
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404"); // Teste si l'erreur est une 404
            /**/
        }

        [TestMethod()]
        public void Post_InvalidObjectPassed_Returns400Error()
        {
            DevisesController controller = new DevisesController(); // Arrange
            Devise rouble = new Devise(4, null, 1510.6);
            var result = controller.Post(rouble); // Act

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un CreatedAtRouteResult"); // Test du type du contenu
            Assert.AreEqual(((BadRequestResult)result.Result).StatusCode, StatusCodes.Status400BadRequest, "Pas 400"); // Teste si l'erreur est une 400
            /**/
        }

        [TestMethod()]
        public void Post_ValidObjectPassed_ReturnsObject()
        {
            DevisesController controller = new DevisesController(); // Arrange
            Devise rouble = new Devise(4, "Rouble", 1510.6);
            var result = controller.Post(rouble); // Act

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtRouteResult"); // Test du type du contenu
            Assert.AreEqual(((CreatedAtRouteResult)result.Result).StatusCode, StatusCodes.Status201Created, "Pas 201"); // Teste si l'erreur est une 201
            Assert.AreEqual(((CreatedAtRouteResult)result.Result).Value, rouble, "Pas les mêmes"); // Teste si la valeur retournée est bien la bonne
            /**/
        }

        [TestMethod()]
        public void Put_IdAndDeviseIDNotEqual_Returns400()
        {
            DevisesController controller = new DevisesController(); // Arrange
            Devise dollar = new Devise(1, "Dollarr", 1.07);
            var result = controller.Put(2, dollar);

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
            Assert.AreEqual(((BadRequestResult)result.Result).StatusCode, StatusCodes.Status400BadRequest, "Pas 400"); // Teste si l'erreur est une bad request
            /**/
        }

        [TestMethod()]
        public void Put_UnknownIDNotEqual_Returns404()
        {
            DevisesController controller = new DevisesController(); // Arrange
            Devise dollar = new Devise(50, "Dollarr", 1.07);
            var result = controller.Put(50, dollar);

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un BadRequestResult");
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404"); // Teste si l'erreur est une not found
            /**/
        }

        [TestMethod()]
        public void Put_UnknownIDNotEqual_Returns204()
        {
            DevisesController controller = new DevisesController(); // Arrange
            Devise dollar = new Devise(50, "Dollarr", 1.07);
            var result = controller.Put(50, dollar); // Act

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un BadRequestResult");
            Assert.AreEqual(((NoContentResult)result.Result).StatusCode, StatusCodes.Status204NoContent, "Pas 204"); // Teste si l'erreur est une no content
            /**/
        }

        [TestMethod()]
        public void Delete_UnknownId_Returns404()
        {
            DevisesController controller = new DevisesController(); // Arrange
            var result = controller.Delete(50);

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un BadRequestResult");
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404"); // Teste si l'erreur est une not found
            /**/
        }

        [TestMethod()]
        public void Delete_ExistingIdPassed_ReturnsObject()
        {
            DevisesController controller = new DevisesController(); // Arrange
            var result = controller.Delete(1); // Act

            /* Asserts */
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); // Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); // Test de la devise récupérée
            /**/
        }
    }
}