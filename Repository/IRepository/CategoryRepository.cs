using MagicVilla.Reposiory;
using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Context;

namespace SistemAdminProducts.Repository.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {
        private readonly ApplicationDdContext _db;
        public CategoryRepository(ApplicationDdContext db) : base(db)
        {
            _db = db;
        }
        public async Task Update(Category entidad)
        {
            entidad.UpdateAt = DateTime.Now;
            _db.Entry(entidad).State = EntityState.Modified;
            await Save();
        }
    }
}
