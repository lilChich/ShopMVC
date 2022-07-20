using AutoMapper;
using ShopMVC.BLL.DTO;
using ShopMVC.BLL.Interfaces.IServices;
using ShopMVC.BLL.Models;
using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Services
{
    public class ShopService : IShopService
    {
        private readonly IProductRepository productRepos;
        public IMapper Mapper { get; set; }

        public ShopService(IProductRepository productRepos, IMapper Mapper)
        {
            this.productRepos = productRepos;
            this.Mapper = Mapper;
        }

        public async Task<ProductMenuModel> LoadProductsAsync(int? type, string name, int page = 1, SortType sort = SortType.PriceAsc, int amountOfElementOnPage = 3)
        {
            var products = await productRepos.GetAsync(i => true);
            if (type != null)
            {
                TypeOfProduct typeProduct = (TypeOfProduct)Enum.ToObject(typeof(TypeOfProduct), type);
                products = products.Where(i => (int)i.Type == type);
            }

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(i => i.Name.Contains(name));
            }

            products = sort switch
            {
                SortType.NameDesc => products.OrderByDescending(i => i.Name),
                SortType.PriceAsc => products.OrderBy(i => i.Price),
                SortType.PriceDesc => products.OrderByDescending(i => i.Price),
                _ => products.OrderBy(i => i.Name),
            };
            var count = products.Count();
            var items = products.Skip((page - 1) * amountOfElementOnPage).Take(amountOfElementOnPage).ToList();

            var mappedItems = Mapper.Map<IEnumerable<ProductDTO>>(items);

            ProductMenuModel viewModel = new ProductMenuModel()
            {
                PageViewModel = new PageModel(count, page, amountOfElementOnPage),
                SortViewModel = new SortViewModel(sort),
                FilterViewModel = new FilterViewModel(type, name),
                Products = mappedItems
            };
            return viewModel;
        }
    }
}
