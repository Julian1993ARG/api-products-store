using MagicVilla.Reposiory;
using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Context;
using SistemAdminProducts.Repository.IRepository;

namespace SistemAdminProducts.Repository
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategory
    {
        private readonly ApplicationDdContext _db;
        public SubCategoryRepository(ApplicationDdContext db) : base(db)
        {
            _db = db;
        }
        public async Task Update(SubCategory entidad)
        {
            entidad.UpdateAt = DateTime.Now;
            _db.Entry(entidad).State = EntityState.Modified;
            await Save();
        }
    }
}
