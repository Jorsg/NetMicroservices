using Catal.APIs.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catal.APIs.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Products>> GetProducts();
		Task<Products> GetProducts(string id);
		Task<IEnumerable<Products>> GetProductsByName(string name);
		Task<IEnumerable<Products>> GetProductsByCategory(string categoryName);

		Task CreateProduct(Products products);
		Task<bool> UpdateProduct(Products products);
		Task<bool> DeleteProduct(string id);
	}
}
