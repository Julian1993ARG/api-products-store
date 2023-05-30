using MagicVilla.Reposiory;
using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Context;

namespace SistemAdminProducts.Repository.IRepository
{
    public class ProductsRepository : Repository<Products>, IProduct
    {
        private readonly ApplicationDdContext _db;
        public ProductsRepository(ApplicationDdContext db) : base(db)
        {
            _db = db;
        }
        public async Task Update(Products entidad)
        {
            entidad.UpdateAt = DateTime.Now;
            _db.Entry(entidad).State = EntityState.Modified;
            await Save();
        }

        public async Task UpdateProductsPriceBySupplierId(int supplierId, double percentage)
        {
            percentage = percentage / 100 + 1;
            await _db.Products.Where(produt => produt.SupplierId == supplierId)
                .ExecuteUpdateAsync(p => p.SetProperty(p => p.CostPrice, p => p.CostPrice * percentage));
        }

        public async Task<IEnumerable<Products>> GetProductsByName(string name)
        {
            name = name.ToUpper();
            var products = await _db.Products
                .Where(p => p.Description
                .ToUpper()
                .Contains(name))
                .Take(5)
                .ToListAsync();
            return products;
        }

    }
}
