using SistemAdminProducts.Models;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface ISupplier : IRepository<Supplier>
    {
        Task Update(Products entidad);
    }
}
