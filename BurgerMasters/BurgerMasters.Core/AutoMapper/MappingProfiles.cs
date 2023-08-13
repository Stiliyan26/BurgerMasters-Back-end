using AutoMapper;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<MenuItem, MenuItemViewModel>()
               .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType.Name))
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
               .ForMember(dest => dest.PortionSize, opt => opt.MapFrom(src => src.PortionSize))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
