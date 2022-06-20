using AutoMapper;
using ShopMVC.BLL.DTO;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skillap.MVC.Maps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<ApplicationRole, UserDTO>()
                .ForMember(DO => DO.Role,
                opt => opt.MapFrom(DTO => DTO.Name))
                .ReverseMap();

            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(DO => DO.Id, opt => opt.MapFrom(DTO => DTO.Id))
                .ForMember(DO => DO.FirstName, opt => opt.MapFrom(DTO => DTO.FirstName))
                .ForMember(DO => DO.SecondName, opt => opt.MapFrom(DTO => DTO.SecondName))
                .ForMember(DO => DO.DateOfBirth, opt => opt.MapFrom(DTO => DTO.DateOfBirth))
                .ForMember(DO => DO.Email, opt => opt.MapFrom(DTO => DTO.UserName))
                .ForMember(DO => DO.Email, opt => opt.MapFrom(DTO => DTO.Email))
                .ForMember(DO => DO.ConfirmedEmail, opt => opt.MapFrom(DTO => DTO.EmailConfirmed))
                .ForMember(DO => DO.Password, opt => opt.MapFrom(DTO => DTO.PasswordHash))
                .ReverseMap();
        }
        
    }
}
