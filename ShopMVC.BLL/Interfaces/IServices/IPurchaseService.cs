using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Interfaces.IServices
{
    public interface IPurchaseService
    {
        public Task<bool> BuyAsync(PurchaseDTO purchaseDto, List<ProductDTO> products);
        public Task<ProductDTO> GetProductByIdAsync(int id);
        public Task<PurchaseMenuModel> GetPurchaseMenuModelAsync(string email, int page, int amountOfElementsOnPage);
    }
}
