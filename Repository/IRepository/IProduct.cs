using SistemAdminProducts.Models;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface IProduct : IRepository<Products>
    {
        Task<Products> Update(Products entidad);
    }
}
