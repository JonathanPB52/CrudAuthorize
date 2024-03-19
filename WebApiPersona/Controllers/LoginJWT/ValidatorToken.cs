using System.Security.Claims;
using WebApiPersona.DataAccess;
using WebApiPersona.Models.Data;

namespace WebApiPersona.Controllers.LoginJWT
{
    public class ValidatorToken
    {
        public readonly CrudSpContext _dbContext;

        public ValidatorToken()
        {
        }
        public ValidatorToken(CrudSpContext _context)
        {
            _dbContext = _context;
        }

        public dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        succes = false,
                        message = "Token invalido",
                        result = ""
                    };
                }
                string id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                //id = int.Parse(id);
                Usuario usuario = _dbContext.Usuarios.FirstOrDefault(x => x.Id.ToString() == id);

                return new
                {
                    succes = true,
                    message = "Token valido",
                    result = usuario
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    succes = false,
                    message = "Error metodo validar Token" + ex.Message,
                    result = ""
                };
            }
        }

    }
}
