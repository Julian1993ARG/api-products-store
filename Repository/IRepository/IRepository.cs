using System.Linq.Expressions;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Save();
        Task Create(T entidad);
        Task Delete(T entidad);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null,
                                 Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T?> Get(Expression<Func<T, bool>> filter = null,
                                 Func<IQueryable<T>, IQueryable<T>> include = null, bool traked = true);
    }
}
