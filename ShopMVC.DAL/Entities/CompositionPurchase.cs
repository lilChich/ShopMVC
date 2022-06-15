using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Entities
{
    public class CompositionPurchase
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
