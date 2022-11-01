using Catal.APIs.Entities;
using Catal.APIs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catal.APIs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var product = await _repository.GetProducts();
            if (product.Count() <= 0)
                return BadRequest();

            return Ok(product);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((typeof(Products)), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Products>> GetProductById(string id)
        {
            var product = await _repository.GetProducts(id);
            if (product is null)
            {
                _logger.LogError($"Product with id: {id}, not found");
                return BadRequest();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductsByCategory(string category)
        {
            var product = await _repository.GetProductsByCategory(category);
            if (product is null)
                return BadRequest();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType((typeof(Products)), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Products>> CreateProduct([FromBody] Products products)
        {
            await _repository.CreateProduct(products);
            return CreatedAtRoute("GetProduct", new { id = products.Id }, products);
        }

        [HttpPut]
        [ProducesResponseType((typeof(Products)), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Products products)
        {
            return Ok(await _repository.UpdateProduct(products));
        }

        [HttpDelete]
        [ProducesResponseType((typeof(Products)), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }



    }
}
