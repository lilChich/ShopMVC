using ShopMVC.DAL.Entities;
using ShopMVC.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopMVC.DAL.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public DataContext bd;
        public PurchaseRepository(DataContext bd)
        {
            this.bd = bd;
        }

        public async Task CreateAsync(params Purchase[] data)
        {
            await bd.Set<Purchase>().AddRangeAsync(data);
            await bd.SaveChangesAsync();
        }

        public async Task UpdateAsync(Purchase purchase)
        {
            bd.Set<Purchase>().Update(purchase);
            await bd.SaveChangesAsync();
        }

        public async Task DeleteAsync(Purchase purchase)
        {
            bd.Set<Purchase>().Remove(purchase);
            await bd.SaveChangesAsync();
        }

        public async Task<Purchase> FindAsync(Expression<Func<Purchase, bool>> predicate)
        {
            return await bd.Set<Purchase>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Purchase>> GetAsync(Expression<Func<Purchase, bool>> predicate)
        {
            return await bd.Set<Purchase>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPageAsync(int skip, int page, Expression<Func<Purchase, bool>> predicate)
        {
            if (predicate == null)
            {
                return await bd.Set<Purchase>().Skip(skip).Take(page).ToListAsync();
            }
            return await bd.Set<Purchase>().Where(predicate).Skip(skip).Take(page).ToListAsync();
        }
    }
}
