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
        public Task<Products> Update(Products entidad)
        {
            throw new NotImplementedException();
        }
        public async Task<Products?> GetByUpcCode(string ucCode)
        {
            return await _db.Products.FirstOrDefaultAsync(v => v.UpcCode == ucCode);
        }
    }
}
