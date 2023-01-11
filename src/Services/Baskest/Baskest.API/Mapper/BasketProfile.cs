using AutoMapper;
using Baskest.API.Entities;
using EventBus.Messages.Events;

namespace Baskest.API.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile() 
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
