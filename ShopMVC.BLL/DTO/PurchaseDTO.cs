using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.DTO
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }

        public List<ProductDTO> Products { get; set; }

        public Nullable<decimal> Total
        {
            get
            {
                return Products.Sum(i => i.Price * i.Amount);
            }
        }
    }
}
