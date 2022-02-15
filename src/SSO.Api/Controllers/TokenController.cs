using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using SSO.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SSO.Api.JwtConfig;
using SSO.API.Services.Interface;

namespace SSO.Api.Controllers
{
    [Authorize("bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUsuarioService _usrService;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public TokenController(
            IConfiguration config,
            IUsuarioService usrService,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _config = config;
            _usrService = usrService;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        [AllowAnonymous]
        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken("fake@email.com");
            return token;
        }

        [HttpGet("me")]
        public ActionResult GetUser()
        {
            var user = User.FindFirstValue(ClaimTypes.Name);
            string email = User.FindFirstValue(ClaimTypes.Email);
            string role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new
            {
                Id = user,
                Email = email,
                Role = role
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromForm] string email, [FromForm] string password)
        {

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                var usuarioBase = _usrService.Verificar(email, password);

                if (usuarioBase == null)
                    return Ok(new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    });

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuarioBase.ID, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBase.ID),
                        new Claim(JwtRegisteredClaimNames.Email, usuarioBase.Email),
                        new Claim(JwtRegisteredClaimNames.Acr, usuarioBase.AccessKey),
                        new Claim("role", usuarioBase.Role)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return Ok(new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                });
            }
            else
            {
                return Ok(new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                });
            }
        }
    }
}