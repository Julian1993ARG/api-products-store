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

    }
}
