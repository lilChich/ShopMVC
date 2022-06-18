using Microsoft.AspNetCore.Identity;
using ShopMVC.BLL.DTO;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Interfaces.IServices
{
    interface IAuthService
    {
        public Task<IdentityResult> AddUserAsync(UserDTO userDto, bool isPersistent);
        public Task<UserDTO> Login(UserDTO userDto);
        public Task SignOut();

        public Task<UserDTO> GetUserAsync(string email);
        public Task<UserDTO> GetUserAsync(ClaimsPrincipal claim);
        public Task<UserDTO> GetUserClaimsAsync(ApplicationUser user);
        public Task<UserDTO> GetUserRoleAsync(ApplicationUser user);
        public Task<bool> UpdateUserAsync(UserDTO userDto);
        public Task<ApplicationUser> GetUserByIdAsync(int id);

        public Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        public Task<int> GetUserByIdAsync(ClaimsPrincipal user);
    }
}
