using ShopMVC.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Interfaces.IServices
{
    public interface IShopService
    {
        Task<ProductMenuModel> LoadProductsAsync(int? type, string name, int page, SortType sort, int amountOfElementOnPage);
    }
}
