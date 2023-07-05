using Catal.APIs.Entities;
using Catal.APIs.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catal.APIs.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public Task CreateProduct(Products products)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> DeleteProduct(string id)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<Products>> GetProducts()
		{
			return await _productRepository.GetProducts();
			
		}

		public Task<Products> GetProducts(string id)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Products>> GetProductsByCategory(string categoryName)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Products>> GetProductsByName(string name)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> UpdateProduct(Products products)
		{
			throw new System.NotImplementedException();
		}
	}
}
