using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.BLL.Interfaces.JWT;
using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using ShopMVC.ViewModels;
using Skillap.BLL.Exceptions;
using Skillap.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService userService;
        private readonly IJwtGenerator jwtGenerator;
        private readonly IMapper mapper;
        private readonly IUnitOfWork UoW;
        private readonly UserManager<ApplicationUser> appUser;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAuthService UserService,
                   IMapper Mapper,
                   IUnitOfWork uow,
                   IJwtGenerator jwtGenerator,
                   UserManager<ApplicationUser> AppUser,
                   SignInManager<ApplicationUser> signInManager,
                   IHostingEnvironment hostingEnvironment)
        {
            userService = UserService;
            mapper = Mapper;
            UoW = uow;
            appUser = AppUser;
            this.hostingEnvironment = hostingEnvironment;
            this.jwtGenerator = jwtGenerator;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            if (loginViewModel.ExternalLogins == null)
            {
                loginViewModel.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            }

            if (loginViewModel.Email == null || loginViewModel.Password == null)
            {
                ModelState.AddModelError("", "These fields must be filled");
            }

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = new UserDTO()
            {
                Email = loginViewModel.Email,
                Password = loginViewModel.Password,
                RememberMe = loginViewModel.RememberMe
            };

            try
            {
                var res = await userService.Login(user);
                if (res.Token != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (ValidationExceptions ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            //ModelState.AddModelError("", "Incorrect data");

            return View(loginViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!new EmailAddressAttribute().IsValid(registerViewModel.Email))
            {
                ModelState.AddModelError("", "Incorrect email");
            }

            if (registerViewModel.ConfirmPassword != registerViewModel.Password)
            {
                ModelState.AddModelError("", "Passwords must be the same");
            }

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            string uniqueFileName = null;

            if (registerViewModel.Image != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + registerViewModel.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                registerViewModel.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            var userDTO = new UserDTO()
            {
                FirstName = registerViewModel.FirstName,
                SecondName = registerViewModel.SecondName,
                DateOfBirth = registerViewModel.DayOfBirth,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                Image = uniqueFileName,
                Role = "User"
            };

            var res = await userService.AddUserAsync(userDTO, registerViewModel.RememberMe);

            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Home", registerViewModel);
            }

            return View(registerViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await userService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var user = await userService.GetUserAsync(this.User.Identity.Name);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with such Id cannot be found";
                return View("");
            }

            var model = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email,
                DayOfBirth = user.DateOfBirth,
                Image = user.Image,
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateUser()
        {
            var user = await userService.GetUserAsync(this.User.Identity.Name);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with such Id cannot be found";
                return View("");
            }

            var model = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email,
                DayOfBirth = user.DateOfBirth,
                ExistingPhotoPath = user.Image
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUser(EditUserViewModel model)
        {
            if (User.IsInRole("User"))
            {
                var user = await userService.GetUserAsync(this.User.Identity.Name);

                string userImage = null;
                string uniqueFileName = null;

                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                else
                {
                    userImage = user.Image;
                }

                var userDto = new UserDTO()
                {
                    Id = await userService.GetUserByIdAsync(this.User),
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Email = model.Email,
                    DateOfBirth = model.DayOfBirth,
                    Role = "User",
                    Image = uniqueFileName ?? userImage
                };

                var updateUser = await userService.UpdateUserAsync(userDto);

                if (updateUser)
                {
                    return RedirectToAction("MyProfile", "Account");
                }
            }

            ModelState.AddModelError("", "Something goes wrong while updating");

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = await userService.GetUserAsync(this.User.Identity.Name);

                var res = await userService.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmNewPassword);

                if (res.Succeeded)
                {
                    await userService.SignOut();
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }
    }
}
