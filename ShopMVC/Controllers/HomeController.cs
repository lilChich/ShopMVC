using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.BLL.Models;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShopService shopService;

        public HomeController(ILogger<HomeController> logger, IShopService shopService)
        {
            _logger = logger;
            this.shopService = shopService;
        }

        public async Task<IActionResult> Index(int? type, string name, int page = 1, SortType sort = SortType.PriceAsc)
        {
            var viewModel = await shopService.LoadProductsAsync(type, name, page, sort, 3);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
