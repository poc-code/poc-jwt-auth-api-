using Microsoft.EntityFrameworkCore;
using Moq;
using SSO.Domain.Model;
using SSO.Infra.Context;
using System;
using System.Data;

namespace SSO.Integration.Test.Repository
{
    public class RepositoryBaseTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _entityOptions;
        private Mock<IDbConnection> _conn;
        private ApplicationDbContext _entityContext;

        public RepositoryBaseTest()
        {
            _entityOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .EnableSensitiveDataLogging()
               .Options;

            _entityContext = new ApplicationDbContext(_entityOptions);

            _entityContext.Usuarios.AddRange(new[] {
                new Usuario{
                    ID = "usuario01",
                    AccessKey = "94be650011cf412ca906fc335f615cdc",
                    Email = "usuario01@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Senha1234".Trim()),
                    Role = "Manager"
                },
                new Usuario{
                    ID = "usuario02",
                    AccessKey = "a5708eceedcb41d2b51011f58f708143",
                    Email = "usuario02@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Senha1234".Trim()),
                    Role = "Operator"
                }
            });
            _entityContext.SaveChanges();
        }

        public DbContextOptions<ApplicationDbContext> EntityOptions => _entityOptions;

        public ApplicationDbContext EntityContext => _entityContext;

        public Mock<IDbConnection> Connection
        {
            get => _conn;
            set
            {
                _conn = value;
            }
        }
    }
}
