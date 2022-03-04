using Catal.APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catal.APIs.Repositories
{
    public interface IProductRepository
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
