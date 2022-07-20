using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task CreateAsync(params Purchase[] data);
        public Task DeleteAsync(Purchase product);
        public Task UpdateAsync(Purchase product);

        public Task<Purchase> FindAsync(Expression<Func<Purchase, bool>> predicate);
        public Task<IEnumerable<Purchase>> GetAsync(Expression<Func<Purchase, bool>> predicate);

        public Task<IEnumerable<Purchase>> GetPageAsync(int amount, int page, Expression<Func<Purchase, bool>> predicate);
    }
}
