using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class Data
    {
        public string Id { get; set; }
        public string Amount { get; set; }
    }

    public class BasketController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPurchaseService purchaseService;
        private readonly IAuthService userService;

        public BasketController(IPurchaseService purchaseService, IAuthService userService,
            IMapper mapper)
        {
            this.purchaseService = purchaseService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Cart()
        {
            var basket = await SessionHelper.GetObjectFromJsonAsync<List<ProductDTO>>(HttpContext.Session, "basket") ?? new List<ProductDTO>();

            ViewBag.total = basket.Sum(i => i.Price * i.Amount);

            return View(basket);
        }

        public async Task<IActionResult> AddProduct(string id)
        {
            List<ProductDTO> basket = await SessionHelper.GetObjectFromJsonAsync<List<ProductDTO>>(HttpContext.Session, "basket") ?? new List<ProductDTO>();

            var product = basket.FirstOrDefault(i => i.Id == int.Parse(id));

            if (product != null)
            {
                product.Amount++;
            }
            else
            {
                product = await purchaseService.GetProductByIdAsync(int.Parse(id));
                basket.Add(product);
            }

            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "basket", basket);

            return RedirectToAction("Cart", "Basket");
        }

        [HttpPost]
        public async Task ChangeProduct([FromBody] Data value)
        {
            List<ProductDTO> basket = await SessionHelper.GetObjectFromJsonAsync<List<ProductDTO>>(HttpContext.Session, "basket") ?? new List<ProductDTO>();

            var product = basket.FirstOrDefault(i => i.Id == int.Parse(value.Id));
            product.Amount = int.Parse(value.Amount);

            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "basket", basket);
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            List<ProductDTO> basket = await SessionHelper.GetObjectFromJsonAsync<List<ProductDTO>>(HttpContext.Session, "basket") ?? new List<ProductDTO>();

            basket.Remove(basket.FirstOrDefault(i => i.Id == int.Parse(id)));
            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "basket", basket);

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> Buy()
        {
            List<ProductDTO> basket = await SessionHelper.GetObjectFromJsonAsync<List<ProductDTO>>(HttpContext.Session, "basket");
            var user = await userService.GetUserAsync(this.User.Identity.Name);

            if (basket == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var purchaseDto = new PurchaseDTO()
            {
                UserId = user.Id,
                Date = DateTime.Now,
                Products = basket
            };

            await purchaseService.BuyAsync(purchaseDto, basket);

            return RedirectToAction("History", "User");
        }
    }
}
