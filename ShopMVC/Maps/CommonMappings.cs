using AutoMapper;
using ShopMVC.BLL.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Maps
{
    public class CommonMappings
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMap());
                cfg.AddProfile(new PurchaseMap());
                cfg.AddProfile(new ProductComPurchaseMap());
            });

            return config;
        }
    }
}
