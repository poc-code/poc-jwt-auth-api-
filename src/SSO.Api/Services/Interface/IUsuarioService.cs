using System.Collections.Generic;
using SSO.Domain.Model;

namespace SSO.API.Services.Interface
{
    public interface IUsuarioService
    {
         Usuario ObterPorId(string id);
         IEnumerable<Usuario> ObterTodos();
         void Incluir (Usuario dadosUsuario);

         Usuario Verificar(string email, string senha);
    }
}