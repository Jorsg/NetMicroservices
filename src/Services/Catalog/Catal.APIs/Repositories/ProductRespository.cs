using Catal.APIs.Data;
using Catal.APIs.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catal.APIs.Repositories
{
    public class ProductRespository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRespository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProduct(Products products)
        {
            await _context.Product.InsertOneAsync(products);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Product
                                                      .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _context.Product
                                 .FindSync(p => true).ToListAsync(); 
        }

        public async Task<Products> GetProducts(string id)
        {
            return await _context.Product
                                 .FindSync(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Products>> GetProductsByCategory(string categoryName)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Category, categoryName);

            return await _context.Product
                                 .FindSync(filter).ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProductsByName(string name)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Name, name);
            return await _context.Product
                                  .FindSync(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Products products)
        {
            var updateResult = await _context.Product
                                             .ReplaceOneAsync(filter: g => g.Id == products.Id, replacement: products );

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
