using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCore.WebAPI.Controllers;
using EFCore.Dominio;
using EFCore.Dominio.Interfaces;

namespace YourNamespace.Tests
{
    public class HeroiControllerTests
    {
        private HeroiController _controller;
        private Mock<IHeroiServices> _mockHeroiServices;

        [SetUp]
        public void Setup()
        {
            _mockHeroiServices = new Mock<IHeroiServices>();
            _controller = new HeroiController(_mockHeroiServices.Object);
        }

        [Test]
        public async Task Get_ReturnsOkResult_WithHerois()
        {
            // Arrange
            var expected = new Heroi[] { new Heroi() };
            _mockHeroiServices.Setup(x => x.GetAllHerois(true)).Returns(Task.FromResult(expected));

            // Act
            IActionResult result = await _controller.Get();

            var OkResult = (OkObjectResult) result;


            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expected, OkResult.Value);
        }

        [Test]
        public async Task Get_ReturnsBadRequestResult_WhenExceptionIsThrown()
        {
            // Arrange
            _mockHeroiServices.Setup(x => x.GetAllHerois(true)).Throws(new System.Exception("Deu erro"));

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Erro: Deu erro", (result as BadRequestObjectResult).Value);

            Assert.AreEqual(400, (result as BadRequestObjectResult).StatusCode);
        }

        [Test]
        public async Task GetById_ReturnsOkResult_WithHeroi()
        {
            // Arrange
            int id = 1;
            var expected = new Heroi() { Id = id };
            _mockHeroiServices.Setup(x => x.GetHeroiById(id, true)).Returns(Task.FromResult(expected));

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expected, (result as OkObjectResult).Value);
        }

        [Test]
        public async Task GetById_ReturnsBadRequestResult_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            _mockHeroiServices.Setup(x => x.GetHeroiById(id, true)).Throws(new System.Exception("Test exception"));

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Erro: Test exception", (result as BadRequestObjectResult).Value);
        }

    }
}