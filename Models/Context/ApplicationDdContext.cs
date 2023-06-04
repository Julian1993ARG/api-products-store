using Microsoft.EntityFrameworkCore;

namespace SistemAdminProducts.Models.Context
{
    public class ApplicationDdContext : DbContext
    {
        public ApplicationDdContext(DbContextOptions<ApplicationDdContext>options):base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        // Comando para ejecutar Migrate : Add-Migration InitialCreate
        // Comando para ejecutar Migrate : Update-Database
        // Comando conectarse a la base de datos Azure
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .HasIndex(p => p.UpcCode)
                .IsUnique();
            modelBuilder.Entity<Products>()
                .HasIndex(p => p.Description)
                .IsUnique();

            var newCategory1 = new Category
            {
                Id = 1,
                Name = "Category 1",
                Description = "Description Category 1",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            var newCategory2 = new Category
            {
                Id = 2,
                Name = "Category 2",
                Description = "Description Category 2",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            }; 
            var newCategory3 = new Category
            {
                Id = 3,
                Name = "Category 3",
                Description = "Description Category 3",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };

            modelBuilder.Entity<Category>().HasData(newCategory1, newCategory2, newCategory3);

            var newSubCategory1 = new SubCategory
            {
                Id = 1,
                Name = "SubCategory 1",
                Description = "Description SubCategory 1",
                CategoryId = 1,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            var newSubCategory2 = new SubCategory
            {
                Id = 2,
                Name = "SubCategory 2",
                Description = "Description SubCategory 2",
                CategoryId = 2,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            var newSubCategory3 = new SubCategory
            {
                Id = 3,
                Name = "SubCategory 3",
                Description = "Description SubCategory 3",
                CategoryId = 3,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };

            modelBuilder.Entity<SubCategory>().HasData(newSubCategory1, newSubCategory2, newSubCategory3);

            var newSupplier1 = new Supplier
            {
                Id = 1,
                Name = "Supplier 1",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,

            };
            var newSupplier2 = new Supplier
            {
                Id = 2,
                Name = "Supplier 2",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            var newSupplier3 = new Supplier
            {
                Id = 3,
                Name = "Supplier 3",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };

            modelBuilder.Entity<Supplier>().HasData(newSupplier1, newSupplier2, newSupplier3);

            var newProduct = new Products
            {
                Id = 1,
                Description = "Product 1",
                UpcCode = "4314556",
                CostPrice = 10.5,
                Proffit = 1.5,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                SupplierId = 1,
                SubCategoryId = 1,
            };
            var newProduct2 = new Products
            {
                Id = 2,
                Description = "Product 2",
                UpcCode = "31467885",
                CostPrice = 10.5,
                Proffit = 1.5,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                SupplierId = 2,
                SubCategoryId = 2,
            };
            var newProduct3 = new Products
            {
                Id = 3,
                Description = "Product 3",
                UpcCode = "12345623",
                CostPrice = 10.5,
                Proffit = 1.5,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                SupplierId = 3,
                SubCategoryId = 3,
            };

            modelBuilder.Entity<Products>().HasData(newProduct, newProduct2, newProduct3);

            
        }
    }
}
