using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataContext db = new DataContext();
        private EFGenericRepository<ApplicationUser> applicationUsers;
        private EFGenericRepository<Product> productsRepository;
        private EFGenericRepository<CompositionPurchase> compositionPurchasesRepository;
        private EFGenericRepository<Purchase> purchasesRepository;

        public EFUnitOfWork()
        {
        }

        public EFGenericRepository<ApplicationUser> ApplicationUsers
        {
            get
            {
                if (this.applicationUsers == null)
                {
                    this.applicationUsers = new EFGenericRepository<ApplicationUser>(db);
                }
                return applicationUsers;
            }
        }

        public EFGenericRepository<Product> Products
        {
            get
            {
                if (this.productsRepository == null)
                {
                    this.productsRepository = new EFGenericRepository<Product>(db);
                }
                return productsRepository;
            }
        }

        public EFGenericRepository<CompositionPurchase> CompositionPurchases
        {
            get
            {
                if (this.compositionPurchasesRepository == null)
                {
                    this.compositionPurchasesRepository = new EFGenericRepository<CompositionPurchase>(db);
                }
                return compositionPurchasesRepository;
            }
        }

        public EFGenericRepository<Purchase> Purchases
        {
            get
            {
                if (this.purchasesRepository == null)
                {
                    this.purchasesRepository = new EFGenericRepository<Purchase>(db);
                }
                return purchasesRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
