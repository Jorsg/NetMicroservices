using Discount.Grcp.Entities;
using Discount.Grcp.Protos;
using AutoMapper;

namespace Discount.Grcp.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
