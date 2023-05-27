using MagicVilla.Reposiory;
using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Context;

namespace SistemAdminProducts.Repository.IRepository
{
    public class SupplierRepository : Repository<Supplier>, ISupplier
    {
        private readonly ApplicationDdContext _db;
        public SupplierRepository(ApplicationDdContext db) : base(db)
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
