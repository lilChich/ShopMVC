using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Models
{
    public class ProductMenuModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public PageModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
