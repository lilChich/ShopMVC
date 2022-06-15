using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Entities
{
    public enum TypeOfProduct
    {
        Phone=1, Computer, Armchair, Mouse, Laptop, Screen
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public TypeOfProduct Type { get; set; }

        public IList<CompositionPurchase> Purchases { get; set; }
    }
}
