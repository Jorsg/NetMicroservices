using Catal.APIs.Controllers;
using Catal.APIs.Entities;
using Catal.APIs.Repositories;
using Catal.APIs.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Catalog.API.Test
{
	[TestFixture]
	public class CatalogControllerTests
	{
		private Mock<IProductService> _productService;
		private Mock<IProductRepository> _productRepository;
		private Mock<ILogger<CatalogController>> _logger;
		private CatalogController _catalogController;

		[SetUp]
		public void Setup()
		{
			_productService = new Mock<IProductService>();
			_productRepository = new Mock<IProductRepository>();
			_logger = new Mock<ILogger<CatalogController>>();
			_catalogController = new CatalogController(_productRepository.Object, _logger.Object, _productService.Object);
		}

		[Test]
		public void GetProducts_WhenCalled_ReturnAllProductByCatalogOk()
		{
			//Arrange
			
			var products = SetupProducts();
			_productRepository.Setup(c => c.GetProducts()).Returns(products);
			//Act
			var result = _catalogController.GetProducts().Result;
			//Assert
			Assert.IsNotNull(result);
			var OkResult = (OkObjectResult)result.Result;
			var responsedProduct = (IEnumerable<Products>)OkResult.Value;

			Assert.That(200, Is.EqualTo(OkResult.StatusCode));
			Assert.That(responsedProduct.FirstOrDefault().Name, Is.EqualTo("Captan America"));
			
		}

		[Test]
		public void GetProducts_WhenCalled_ReturnBadRequets()
		{
			var products = SetupErrorProduct();
			_productRepository.Setup(c => c.GetProducts()).Throws<WebException>();
			var result = _catalogController.GetProducts().IsFaulted;

			Assert.IsNotNull(result);
			Assert.That(result, Is.True);
		}


		private async Task<IEnumerable<Products>> SetupProducts()
		{
			var products = new List<Products>();


			products.Add(new Products { Category = "toy", Description = "captan American", Id = "23", Name = "Captan America", Price = 23, Summary = "Toy to all children", ImageFile = "img/captan.jpg" });
			products.Add(new Products { Category = "home", Description = "bed", Id = "40", Name = "cover bed", Price = 78, Summary = "cover bed", ImageFile = "img/bed.png" });
			products.Add(new Products { Category = "kitchen", Description = "knif", Id = "56", Name = "knife", Price = 10, Summary = "nothing", ImageFile = "img/knif.kpg" });

			return products;
		}

		private async Task<IEnumerable<Products>> SetupErrorProduct()
		{
			var products = new List<Products>();
			products.Add(new Products { Category = "toy", Description = "captan American", Id = "23", Name = "Captan America", Summary = "Toy to all children", ImageFile = "img/captan.jpg" });
			return products.ToList();

		}
	}
}