using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models.Context;
using SistemAdminProducts.Repository.IRepository;
using System.Linq.Expressions;

namespace MagicVilla.Reposiory
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDdContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDdContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Save() => await _db.SaveChangesAsync();

        public async Task Create(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Save();
        }

        public async Task Delete(T entidad)
        {
            dbSet.Entry(entidad).State = EntityState.Deleted;
            await Save();
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter = null,
                                 Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = dbSet;
            //if (!tracked)
            //{
            //    query = query.AsNoTracking();
            //}
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

    }
}
