using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiPersona.DataAccess;
using WebApiPersona.Models.Data;
using WebApiPersona.Models.DTO.ModelsJWT;

namespace WebApiPersona.Controllers.LoginJWT
{
    [Route("api/Login")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly CrudSpContext _dbContext;
        public IConfiguration _configuration;

        public UsuarioController(CrudSpContext _context, IConfiguration configuration)
        {
            _dbContext = _context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public dynamic IniciarSesion([FromBody] UserJwt Data)
        {
            string user = Data.Usuario;
            string pass = Data.Passwork;

            Usuario usuario = _dbContext.Usuarios.FirstOrDefault(x => x.NombreU.Trim() == user && x.ContrasenaU.Trim() == pass);
            usuario.NombreU = usuario.NombreU.Trim();
            usuario.ContrasenaU = usuario.ContrasenaU.Trim();

            if (usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrestas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("JWT").Get<Jwt>();

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));
            claims.AddClaim(new Claim("id", usuario.Id.ToString()));
            claims.AddClaim(new Claim("NombreUsu", usuario.NombreU));
            claims.AddClaim(new Claim("Rol", usuario.IdRol.ToString()));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = singIn
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(token);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);


            return new
            {
                success = true,
                mesagge = "Logueo Exitoso",
                result = tokenCreado
            };


        }

    }
}
