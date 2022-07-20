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
    public class CompositionPurchaseRepository : ICompositionPurchaseRepository
    {
        public DataContext bd;
        public CompositionPurchaseRepository(DataContext bd)
        {
            this.bd = bd;
        }

        public async Task CreateAsync(params CompositionPurchase[] data)
        {
            await bd.Set<CompositionPurchase>().AddRangeAsync(data);
            await bd.SaveChangesAsync();
        }

        public async Task UpdateAsync(CompositionPurchase compositionPurchase)
        {
            bd.Set<CompositionPurchase>().Update(compositionPurchase);
            await bd.SaveChangesAsync();
        }

        public async Task DeleteAsync(CompositionPurchase compositionPurchase)
        {
            bd.Set<CompositionPurchase>().Remove(compositionPurchase);
            await bd.SaveChangesAsync();
        }

        public async Task<CompositionPurchase> FindAsync(Expression<Func<CompositionPurchase, bool>> predicate)
        {
            return await bd.Set<CompositionPurchase>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<CompositionPurchase>> GetAsync(Expression<Func<CompositionPurchase, bool>> predicate)
        {
            return await bd.Set<CompositionPurchase>().Where(predicate).ToListAsync();
        }
    }
}
