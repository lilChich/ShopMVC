using Microsoft.AspNetCore.Identity;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.DAL;
using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using ShopMVC.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork uow;
        private readonly DataContext context;
        private readonly EFGenericRepository<Purchase> repositoryPurchase;
        private readonly EFGenericRepository<CompositionPurchase> repositoryComPurchase;

        public UserManager<ApplicationUser> userManager { get; set; }

        public PurchaseService(IUnitOfWork uow, UserManager<ApplicationUser> userManager,
            DataContext context)
        {
            this.uow = uow;
            this.userManager = userManager;
            this.context = context;
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

        public async Task<PurchaseDTO> GetPurchasesAsync(string email, int page = 1, int amountOfElementsOnPage = 3)
        {
            var purchasesList = new List<PurchaseDTO>();
            var user = await userManager.FindByEmailAsync(email);
            var purchases = await uow.Purchases.GetPageAsync((page - 1) * amountOfElementsOnPage, amountOfElementsOnPage, x => x.UserId == user.Id);
            var count = purchases?.Count() ?? 0;

            foreach (var purchase in purchases)
            {
                var products = (await uow.CompositionPurchases.FindAsync(x => x.PurchaseId == purchase.Id))
                         .Join(await uow.Products.FindAsync(x => true),
                            cp => cp.ProductId,
                            p => p.Id,
                            (cp, p) => new ProductDTO
                            {
                                Id = p.Id,
                                Amount = cp.Amount,
                                Name = p.Name,
                                Manufacturer = p.Manufacturer,
                                Price = p.Price,
                                Type = p.Type
                            }
                        );
                purchasesList.Add(new PurchaseDTO()
                {
                    Id = purchase.Id,
                    Date = purchase.Date,
                    Products = products.ToList()
                });
            }

            return new PurchaseMenuModel()
            {
                Purchases = purchasesList,
                PageModel = new PageModel(count, page, amountOfElementsOnPage)
            };
        }
    }
}
