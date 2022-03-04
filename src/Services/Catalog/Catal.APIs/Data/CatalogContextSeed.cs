using Catal.APIs.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catal.APIs.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Products> productCollection)
        {
            bool existsProduct = productCollection.Find(p => true).Any();
            if (!existsProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Products> GetPreconfiguredProducts()
        {
            return new List<Products>()
            {
                new Products()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smarphone in years.",
                    Description = "Lorem ipsum dolor sit amet, consectetur elit. Ut, tenetur natus",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Products()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smarphone in years.",
                    Description = "Lorem ipsum dolor sit amet, consectetur elit. Ut, tenetur natus",
                    ImageFile = "product-2.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                 new Products()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smarphone in years.",
                    Description = "Lorem ipsum dolor sit amet, consectetur elit. Ut, tenetur natus",
                    ImageFile = "product-3.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                   new Products()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship smarphone in years.",
                    Description = "Lorem ipsum dolor sit amet, consectetur elit. Ut, tenetur natus",
                    ImageFile = "product-4.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                }
            };
        }
    }
}
