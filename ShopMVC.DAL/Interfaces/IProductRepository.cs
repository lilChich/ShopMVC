using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.DAL.Interfaces
{
    public interface IProductRepository
    {
        public Task CreateAsync(params Product[] data);
        public Task DeleteAsync(Product product);
        public Task UpdateAsync(Product product);

        public Task<Product> FindAsync(Expression<Func<Product, bool>> predicate);
        public Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> predicate);

    }
}
