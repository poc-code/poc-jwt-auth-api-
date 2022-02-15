using SSO.API.Services.Interface;
using SSO.Domain.Model;
using SSO.Infra.Context;

namespace SSO.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Usuario ObterPorId(string id){
            return _context.Usuarios.FirstOrDefault(u => u.ID == id);
        }

        public IEnumerable<Usuario> ObterTodos(){
            return _context.Usuarios;
        }

        public void Incluir (Usuario dadosUsuario){

            dadosUsuario.Password = BCrypt.Net.BCrypt.HashPassword(dadosUsuario.Password);

            _context.Usuarios.Add(dadosUsuario);
            _context.SaveChanges();
        }

        public Usuario Verificar(string email, string senha)
        {

            var usr = _context.Usuarios.Where(u => u.Email == email).FirstOrDefault();

            // check account found and verify password
            if (usr == null || !BCrypt.Net.BCrypt.Verify(senha, usr.Password))
            {
                var testeHash = BCrypt.Net.BCrypt.HashPassword(senha);
                var verificaTest = BCrypt.Net.BCrypt.Verify(senha, testeHash);

                // authentication failed
                return null;
            }
            else
            {
                // authentication successful
                return usr;
            }
        }
    }
}