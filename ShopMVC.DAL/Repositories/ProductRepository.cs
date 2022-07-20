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
    public class ProductRepository : IProductRepository
    {
        public DataContext bd;
        public ProductRepository(DataContext bd)
        {
            this.bd = bd;
        }

        public async Task CreateAsync(params Product[] data)
        {
            await bd.Set<Product>().AddRangeAsync(data);
            await bd.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            bd.Set<Product>().Update(product);
            await bd.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            bd.Set<Product>().Remove(product);
            await bd.SaveChangesAsync();
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await bd.Set<Product>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> predicate)
        {
            return await bd.Set<Product>().Where(predicate).ToListAsync();
        }
    }
}
