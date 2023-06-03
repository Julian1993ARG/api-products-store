using SistemAdminProducts.Models;

namespace SistemAdminProducts.Repository.IRepository
{
    public interface ICategory : IRepository<Category>
    {
        Task Update(Category entidad);
    }
}
