using Microsoft.EntityFrameworkCore;
using SSO.Domain.Model;
using SSO.Infra.DataSeed;

namespace SSO.Infra.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.ID);

            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                ID = "usuario01",
                AccessKey = "94be650011cf412ca906fc335f615cdc",
                Email = "usuario01@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Senha1234".Trim()),
                Role = "Manager"
            });
        }
    }
}