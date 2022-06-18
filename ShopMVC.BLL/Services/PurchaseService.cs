using Microsoft.AspNetCore.Identity;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork uow;
        public UserManager<ApplicationUser> userManager { get; set; }

        public PurchaseService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            this.uow = uow;
            this.userManager = userManager;
        }

        public async Task<bool> BuyAsync(string email, List<ProductDTO> products)
        {
            var user = await userManager.FindByEmailAsync(email);

            var purchase = new Purchase()
            {
                User = user,
                Date = DateTime.Now
            };

            await uow.Purchases.CreateAsync(purchase);

            var product = (await uow.Products.FindAsync(i => products.Select(j => j.Id).Contains(i.Id))).ToList();

            List<CompositionPurchase> compositionPurchases = new List<CompositionPurchase>(product.Count);

            for (int i = 0; i < product.Count; i++)
            {
                compositionPurchases.Add(new CompositionPurchase()
                {
                    Purchase = purchase,
                    Product = product[i],
                    Amount = products.FirstOrDefault(j => j.Id == product[i].Id).Amount
                });
            }
            try
            {
                await uow.CompositionPurchases.CreateAsync(compositionPurchases.ToArray());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await uow.Products.GetAsync(i => i.Id == id);

            return new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Type = product.Type,
                Manufacturer = product.Manufacturer,
                Amount = 1
            };
        }
    }
}
