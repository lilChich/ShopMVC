using ShopMVC.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Models
{
    public class PurchaseMenuModel
    {
        public IEnumerable<PurchaseDTO> Purchases { get; set; }
        public PageModel PageModel { get; set; }
    }
}
