using SistemAdminProducts.Models;
using System.Linq.Expressions;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface IProduct : IRepository<Products>
    {
        Task Update(Products entidad);
        Task<IEnumerable<Products>> GetProductsByName(string details);
        Task UpdateProductsPriceBySupplierId(int idSupplier, double percentage);
        Task<IEnumerable<Products>> GetPaginateProduts(int page, int pageSize, Expression<Func<Products, bool>> filter = null, Func<IQueryable<Products>, IQueryable<Products>> include = null);
    }
}
