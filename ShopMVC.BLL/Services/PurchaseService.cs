using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.BLL.Models;
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
        public IMapper Mapper { get; set; }
        public UserManager<ApplicationUser> userManager { get; set; }

        public PurchaseService(IUnitOfWork uow, UserManager<ApplicationUser> userManager,
            DataContext context, IMapper Mapper)
        {
            this.uow = uow;
            this.userManager = userManager;
            this.context = context;
            this.Mapper = Mapper;
        }

        public async Task<bool> BuyAsync(PurchaseDTO purchaseDto, List<ProductDTO> products)
        {
            //var mappedUser = Mapper.Map<ApplicationUser>(userDto);
            //var user = await uow.ApplicationUsers.GetAsync(x => x.Email == userDto.Email);

            var mappedPurchase = Mapper.Map<Purchase>(purchaseDto);

            /*var purchase = new Purchase()
            {
                User = user,
                Date = DateTime.Now
            };*/


            await uow.Purchases.CreateAsync(mappedPurchase);

            var product = (await uow.Products.FindAsync(i => products.Select(j => j.Id).Contains(i.Id))).ToList();

            List<CompositionPurchase> compositionPurchases = new List<CompositionPurchase>(product.Count);

            for (int i = 0; i < product.Count; i++)
            {
                compositionPurchases.Add(new CompositionPurchase()
                {
                    Purchase = mappedPurchase,
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
                Amount = 1,
                Description = product.Description,
                Image = product.Image
            };
        }

        public async Task<PurchaseMenuModel> GetPurchasesAsync(string email, int page = 1, int amountOfElementsOnPage = 3)
        {
            var purchasesList = new List<PurchaseDTO>();
            var user = await userManager.FindByEmailAsync(email);
            //var purchases = await uow.Purchases.GetPageAsync((page - 1) * amountOfElementsOnPage, amountOfElementsOnPage, x => x.UserId == user.Id);
            var userPurchases = await uow.Purchases.FindAsync(i => i.UserId == user.Id);            
            var count = userPurchases?.Count() ?? 0;
            var purchases = userPurchases.Skip((page - 1) * amountOfElementsOnPage).Take(amountOfElementsOnPage).ToList();

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
                                Description = p.Description,
                                Type = p.Type,
                                Image = p.Image
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
