﻿using AutoMapper;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            //CreateMap<Order, OrderVm>().ReverseMap();
        }
    }
}
