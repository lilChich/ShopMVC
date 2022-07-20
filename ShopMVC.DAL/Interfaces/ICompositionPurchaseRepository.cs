using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Interfaces
{
    public interface ICompositionPurchaseRepository
    {
        public Task CreateAsync(params CompositionPurchase[] data);
        public Task DeleteAsync(CompositionPurchase product);
        public Task UpdateAsync(CompositionPurchase product);

        public Task<CompositionPurchase> FindAsync(Expression<Func<CompositionPurchase, bool>> predicate);
        public Task<IEnumerable<CompositionPurchase>> GetAsync(Expression<Func<CompositionPurchase, bool>> predicate);
    }
}
