using AutoMapper;
using ShopMVC.BLL.DTO;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Maps
{
    public class PurchaseMap : Profile
    {
        public PurchaseMap()
        {
            CreateMap<Purchase, PurchaseDTO>()
                 .ForMember(DTO => DTO.Id, opt => opt.MapFrom(DO => DO.Id))
                 .ForMember(DTO => DTO.Date, opt => opt.MapFrom(DO => DO.Date))
                 .ForMember(DTO => DTO.User, opt => opt.MapFrom(DO => DO.User))
                 .ForMember(DTO => DTO.UserId, opt => opt.MapFrom(DO => DO.UserId))
                 .ForMember(DTO => DTO.Products, opt => opt.MapFrom(DO => DO.Products))
                 .ReverseMap();

        }
    }
}
