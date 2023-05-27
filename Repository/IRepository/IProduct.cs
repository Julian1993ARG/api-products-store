using SistemAdminProducts.Models;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface IProduct : IRepository<Products>
    {
        Task Update(Products entidad);
        Task<IEnumerable<Products>> GetProductsByName(string details);
    }
}
