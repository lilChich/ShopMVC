using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<CompositionPurchase> Products { get; set; }
    }
}
