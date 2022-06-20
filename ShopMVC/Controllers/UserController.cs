using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.BLL.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class UserController : Controller
    {
        IPurchaseService purchaseService;

        public UserController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> History(int page = 1)
        {
            var purchases = await purchaseService.GetPurchasesAsync(User.Identity.Name, page, 2);
            return View(purchases);
        }
    }
}
