using Microsoft.EntityFrameworkCore;

namespace SistemAdminProducts.Models.Context
{
    public class ApplicationDdContext : DbContext
    {
        public ApplicationDdContext(DbContextOptions<ApplicationDdContext>options):base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
        // Comando para ejecutar Migrate : Add-Migration InitialCreate
        // Comando para ejecutar Migrate : Update-Database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .HasIndex(p => p.UpcCode)
                .IsUnique();
            modelBuilder.Entity<Products>()
                .HasIndex(p => p.Decription)
                .IsUnique();

        }
    }
}
