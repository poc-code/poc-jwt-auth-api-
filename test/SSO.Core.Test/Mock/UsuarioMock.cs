using Bogus;
using SSO.Domain.Model;
using System;

namespace SSO.Core.Test.Mock
{
    public static class UsuarioMock
    {
        public static Faker<Usuario> Fake => new Faker<Usuario>().CustomInstantiator(f => new Usuario
        {
            ID = f.Person.UserName,
            Email = f.Person.Email,
            AccessKey = Guid.NewGuid().ToString("N"),
            Password = BCrypt.Net.BCrypt.HashPassword("Senha1234".Trim()),
            Role = f.Random.Word()
        });
    }
}
