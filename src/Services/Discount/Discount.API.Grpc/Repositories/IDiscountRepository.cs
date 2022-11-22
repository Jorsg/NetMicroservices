using Discount.API.Grcp.Entities;
using System.Threading.Tasks;

namespace Discount.API.Grcp.Repositories
{
    public interface IDiscountRepository
    {

        Task<Coupon> GetDiscount(string productName);

        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}
