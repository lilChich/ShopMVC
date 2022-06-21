using AutoMapper;
using ShopMVC.BLL.DTO;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Maps
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(DTO => DTO.Id, opt => opt.MapFrom(DO => DO.Id))
                .ForMember(DTO => DTO.Name, opt => opt.MapFrom(DO => DO.Name))
                .ForMember(DTO => DTO.Description, opt => opt.MapFrom(DO => DO.Description))
                .ForMember(DTO => DTO.Price, opt => opt.MapFrom(DO => DO.Price))
                .ForMember(DTO => DTO.Manufacturer, opt => opt.MapFrom(DO => DO.Manufacturer))
                .ForMember(DTO => DTO.Image, opt => opt.MapFrom(DO => DO.Image))
                .ForMember(DTO => DTO.Type, opt => opt.MapFrom(DO => DO.Type))
                .ReverseMap();
        }
    }
}
