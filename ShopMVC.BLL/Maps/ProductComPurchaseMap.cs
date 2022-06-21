using AutoMapper;
using ShopMVC.BLL.DTO;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Maps
{
    public class ProductComPurchaseMap : Profile
    {
        public ProductComPurchaseMap()
        {
            CreateMap<CompositionPurchase, ProductDTO>()
                .ForMember(DTO => DTO.Id, opt => opt.MapFrom(DO => DO.ProductId))
                .ReverseMap();
        }
    }
}
