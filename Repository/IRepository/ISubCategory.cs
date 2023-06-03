using SistemAdminProducts.Models;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface ISubCategory : IRepository<SubCategory>
    {
        Task Update(SubCategory entidad);
    }
}
