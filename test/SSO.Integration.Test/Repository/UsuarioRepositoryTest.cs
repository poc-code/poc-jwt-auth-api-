using FluentAssertions;
using SSO.Core.Test.Mock;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;

namespace SSO.Integration.Test.Repository
{
    public class UsuarioRepositoryTest : RepositoryBaseTest
    {

        private readonly ITestOutputHelper _output;

        public UsuarioRepositoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UsuarioRepository_GetAll_Should_NotNull_Return()
        {
            var rs = EntityContext.Usuarios;
            rs.Should().NotBeNull();

            _output.WriteLine($"Sistema operacional: {RuntimeInformation.OSDescription}");
            _output.WriteLine(message: $"Resultado: {rs?.FirstOrDefault().Email}");
        }

        [Fact]
        public void UsuarioRepository_Add_Should_NotNull_Return()
        {
            var data = UsuarioMock.Fake.Generate();

            var rs = EntityContext.Add(data);
            EntityContext.SaveChanges();

            var newData = EntityContext.Usuarios.LastOrDefault();

            newData?.ID.Should().BeSameAs(data.ID);

            _output.WriteLine(message: $"Resultado: {newData?.ID} = {data.ID}");
        }

        [Fact]
        public void UsuarioRepository_Look_For_Email_Should_NotNull_Return()
        {
            var email = "usuario01@email.com";
            var rs = EntityContext.Usuarios.FirstOrDefault(x => x.Email == email);
            rs.Should().NotBeNull();

            _output.WriteLine(message: $"Resultado: {rs?.ID}");
        }
    }
}
