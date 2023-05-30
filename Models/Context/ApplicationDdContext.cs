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

        }
    }
}
