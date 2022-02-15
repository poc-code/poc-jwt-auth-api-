using Microsoft.AspNetCore.Mvc;
using SSO.API.Services.Interface;
using SSO.Domain.Model;

namespace SSO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController: ControllerBase
    {

        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }
        
        
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            var objMessage = new
            {
                message = string.Empty
            };

            if (usuario != null && !string.IsNullOrWhiteSpace(usuario.ID))
            {
                var usuarioBase = _service.ObterPorId(usuario.ID);

                if (usuarioBase == null)
                {
                    _service.Incluir(usuario);
                    objMessage = new { message = "Usuario adicionado com sucesso!" };
                }
                else
                {
                    objMessage = new { message = "Este usuário já existe!" };
                }

                return Ok(objMessage);
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var objMessage = new
            {
                message = string.Empty,
                resultado = new List<Usuario>()
            };
            var usuarioBase = _service.ObterTodos();
            if (usuarioBase.Any())
            {
                objMessage = new
                {
                    message = $"{usuarioBase.Count()} resultados encontrados!",
                    resultado = usuarioBase.ToList()
                };
            }
            else
            {
                objMessage = new
                {
                    message = "Nenhum resultado encontrado!",
                    resultado = new List<Usuario>()
                };
            }

            return Ok(objMessage);
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] string id)
        {
            var objMessage = new
            {
                message = string.Empty,
                resultado = new Usuario()
            };
            var usuarioBase = _service.ObterPorId(id);
            if (usuarioBase == null)
            {
                return NoContent();
            }
            else
            {
                objMessage = new
                {
                    message = "Usuário encontrado!",
                    resultado = usuarioBase
                };
            }

            return Ok(objMessage);
        }
    }
}