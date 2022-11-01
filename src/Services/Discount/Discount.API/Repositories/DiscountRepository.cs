using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connetion = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connetion.ExecuteAsync
                ("Insert into Coupon(ProductName, Description, Amount) Values (@ProductName, @Description, @Amount)",
                new {ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount});

            if(affected is 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM coupon WHERE productname = @productname",
                new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connetion = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connetion.QueryFirstOrDefaultAsync<Coupon>
                ("Select * From coupon Where productname = @productname", new { ProductName = productName });

            if (coupon is null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

             return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE coupon SET productname=@productname, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
