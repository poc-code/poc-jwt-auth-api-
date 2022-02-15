using SSO.Domain.Model;
using SSO.Infra.Context;

namespace SSO.Infra.DataSeed
{
    public static class DataSeeder 
    {
        public static void Seed(this ApplicationDbContext context)
        {

            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario
            {
                ID = "usuario01",
                AccessKey = "94be650011cf412ca906fc335f615cdc",
                Email = "usuario01@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Senha1234".Trim()),
                Role = "Manager"
            });

            usuarios.Add(new Usuario
            {
                ID = "usuario02",
                AccessKey = "531fd5b19d58438da0fd9afface43b3c",
                Password = BCrypt.Net.BCrypt.HashPassword("Senha1234"),
                Email = "usuario02@email.com",
                Role = "Operator"
            });

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }

    }
}
