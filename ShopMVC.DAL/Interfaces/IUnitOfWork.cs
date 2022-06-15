using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        EFGenericRepository<ApplicationUser> ApplicationUsers { get; }
        EFGenericRepository<Product> Products { get; }
        EFGenericRepository<CompositionPurchase> CompositionPurchases { get; }
        EFGenericRepository<Purchase> Purchases { get; }
        void Save();
    }
}
