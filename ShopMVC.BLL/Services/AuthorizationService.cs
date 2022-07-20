using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.DAL;
using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using Skillap.BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Services
{
    public class AuthorizationService : IAuthService
    {
        public IMapper Mapper { get; set; }
        public DataContext Context { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public RoleManager<ApplicationRole> RoleManager { get; set; }

        public AuthorizationService(IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            DataContext context)
        {
            Mapper = mapper;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            Context = context;
        }

        public async Task<UserDTO> Login(UserDTO userDto)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(userDto.Email);

                if (user == null)
                {
                    throw new ValidationExceptions("Cannot find such a user", "");               
                }

                var result = await SignInManager.PasswordSignInAsync(user, userDto.Password, userDto.RememberMe, false);

                if (result.Succeeded)
                {
                    return new UserDTO
                    {
                        FirstName = user.FirstName,
                        SecondName = user.SecondName,
                        DateOfBirth = user.DateOfBirth,
                        Token = userDto.Token,
                        Image = user.Image
                    };
                }
                else
                {
                    throw new ValidationExceptions("", "");
                }
            }
            catch
            {
                throw new ValidationExceptions("Password or Email is incorrect", "");
            }

            //return userDto;
        }

        public async Task<IdentityResult> AddUserAsync(UserDTO userDto, bool isPersistent)
        {
            var user = Mapper.Map<ApplicationUser>(userDto);
            var res = await UserManager.CreateAsync(user, user.PasswordHash);

            if (!res.Succeeded)
            {

                return res;
            }

            await SignInManager.SignInAsync(user, isPersistent);

            if (!await RoleManager.RoleExistsAsync("Admin"))
            {
                await RoleManager.CreateAsync(new ApplicationRole() { Name = "Admin" });
                await Login(userDto);
            }
            if (userDto.Email.Contains("noobbogdan@gmail.com"))
            {
                await UserManager.AddToRoleAsync(user, "Admin");
            }
            if (!await RoleManager.RoleExistsAsync("User"))
            {
                await RoleManager.CreateAsync(new ApplicationRole() { Name = "User" });
            }
            if (userDto.Email.Contains("@"))
            {
                await UserManager.AddToRoleAsync(user, "User");
            }

            return res;
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserDTO userDto, string currentPassword, string newPassword)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(userDto.Email);

                if (user == null)
                {
                    throw new ValidationExceptions("Cannot find such a user", "");
                }

                var res = await UserManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (!res.Succeeded)
                {
                    return res;
                }

                return res;
            }
            catch
            {
                throw new ValidationExceptions("Something goes wrong while updating", "");
            }
        }

        public async Task SignOut()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<UserDTO> GetUserAsync(string email)
        {
            try
            {
                return Mapper.Map<UserDTO>(await UserManager.FindByEmailAsync(email));
            }
            catch
            {
                return new UserDTO();
            }
        }

        public async Task<int> GetUserByIdAsync(ClaimsPrincipal user) => (await UserManager.GetUserAsync(user)).Id;

        public async Task<UserDTO> GetUserAsync(ClaimsPrincipal claim) => Mapper.Map<UserDTO>(await UserManager.GetUserAsync(claim));

        public async Task<UserDTO> GetUserClaimsAsync(ApplicationUser user) => (UserDTO)await UserManager.GetClaimsAsync(user);

        public async Task<UserDTO> GetUserRoleAsync(ApplicationUser user) => (UserDTO)await UserManager.GetRolesAsync(user);

        public async Task<bool> UpdateUserAsync(UserDTO userDto)
        {
            var user = await UserManager.FindByEmailAsync(userDto.Email);

            if ((user != null) && (user.Id != userDto.Id))
            {
                return false;
            }

            try
            {
                user.Id = userDto.Id;
                user.FirstName = userDto.FirstName;
                user.SecondName = userDto.SecondName;
                user.DateOfBirth = userDto.DateOfBirth;
                user.Email = userDto.Email;
                user.Image = userDto.Image;

                await UserManager.UpdateAsync(user);
            }
            catch
            {
                throw new ValidationExceptions("Cannot update user", "");
            }

            await SignInManager.RefreshSignInAsync(user);

            return true;
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            var res = await UserManager.DeleteAsync(user);

            return res;
        }

        
    }
}
