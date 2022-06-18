using ShopMVC.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Interfaces.IServices
{
    interface IPurchaseService
    {
        public Task<bool> BuyAsync(string email, List<ProductDTO> products);
        public Task<ProductDTO> GetProductByIdAsync(int id);
    }
}
