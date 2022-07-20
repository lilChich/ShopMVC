using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.BLL.Interfaces.IServices;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IPurchaseService purchaseService;

        public UserController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> History(int page = 1)
        {
            var purchases = await purchaseService.GetPurchaseMenuModelAsync(User.Identity.Name, page, 3);
            return View(purchases);
        }
    }
}
