using MagicVilla.Reposiory;
using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Context;
using SistemAdminProducts.Repository.IRepository;
using System.Linq.Expressions;

namespace SistemAdminProducts.Repository
{
    public class ProductsRepository : Repository<Products>, IProduct
    {
        private readonly ApplicationDdContext _db;
        public ProductsRepository(ApplicationDdContext db) : base(db)
        {
            _db = db;
        }
        public async Task Update(Products entidad)
        {
            entidad.UpdateAt = DateTime.Now;
            _db.Entry(entidad).State = EntityState.Modified;
            await Save();
        }

        public async Task UpdateProductsPriceBySupplierId(int supplierId, double percentage)
        {
            percentage = percentage / 100 + 1;
            await _db.Products.Where(produt => produt.SupplierId == supplierId)
                .ExecuteUpdateAsync(p => p.SetProperty(p => p.CostPrice, p => p.CostPrice * percentage));
        }

        public async Task<IEnumerable<Products>> GetProductsByName(string name)
        {
            name = name.ToUpper();
            var products = await _db.Products
                .Where(p => p.Description
                .ToUpper()
                .Contains(name))
                .Take(5)
                .ToListAsync();
            return products;
        }

        public async Task<(IEnumerable<Products> items, int totalPages, int totalRecords)> GetPaginateProduts(
            int page, int pageSize,
            Func<IQueryable<Products>,
                IQueryable<Products>> filter = null,
            Func<IQueryable<Products>,
                IQueryable<Products>> include = null
            )
        {
            IQueryable<Products> products = _db.Set<Products>();
            if (filter != null) products = filter(products);

            if (include != null) products = include(products);

            try
            {
                IEnumerable<Products> items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                int totalRecords = await products.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                return (items, totalPages, totalRecords);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al obtener productos paginados: " + ex.Message);
                return (Enumerable.Empty<Products>(), 0, 0);
            };

        }



    }
}
