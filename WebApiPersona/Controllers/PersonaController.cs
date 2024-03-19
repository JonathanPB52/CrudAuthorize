using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiPersona.DataAccess;
using WebApiPersona.Models.Contracts;
using WebApiPersona.Models.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPersona.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ICrudPersona _crudPersona;
        public PersonaController(ICrudPersona crudPersona)
        {
            _crudPersona = crudPersona;
        }

        // GET: api/<PersonaController>
        [HttpGet]
        [Route("ListarPersonas")]
        public dynamic GetPersona()
        {
            try
            {
                var lis = _crudPersona.ConsultarPersona();
                return lis;
            }
            catch(Exception ex)
            {
                return new { success = true, mesagge = "Error servicio consultar persona"+ex.Message };
            }
        }

        [HttpPost]
        [Route("CrearPersona")]
        public dynamic PostPersona(Persona persona)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var Rol = identity.Claims.FirstOrDefault(x => x.Type == "Rol").Value;

                if (Rol == "1" || Rol == "2")
                {
                    var personaI = _crudPersona.CrearPersona(persona);
                    return personaI;
                }
                else
                {
                    return new { success = true, mesagge = "No cuenta con permisos para este servicio" };
                }
            }
            catch (Exception ex)
            {
                return new { success = true, mesagge = "Error servicio crear persona" + ex.Message };
            }
        }

        [HttpDelete]
        [Route("EliminarPersona")]
        public dynamic deletePersona(int Id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var Rol = identity.Claims.FirstOrDefault(x => x.Type == "Rol").Value;

                if (Rol == "1" || Rol == "2")
                {
                    var Resp = _crudPersona.EliminarPersona(Id);
                    return Resp;
                }
                else
                {
                    return new { success = true, mesagge = "No cuenta con permisos para este servicio" };
                }
            }
            catch (Exception ex)
            {
                return new { success = true, mesagge = "Error servicio eliminar persona" + ex.Message };
            }
        }

        [HttpPut]
        [Route("EditarPersona")]
        public dynamic EditPersona(int Id, Persona obj)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var Rol = identity.Claims.FirstOrDefault(x => x.Type == "Rol").Value;

                if (Rol == "1" || Rol == "2")
                {
                    var Resp = _crudPersona.ActualizarPersona(Id, obj);
                    return Resp;
                }
                else
                {
                    return new { success = true, mesagge = "No cuenta con permisos para este servicio" };
                }
            }
            catch (Exception ex)
            {
                return new { success = true, mesagge = "Error servicio editar persona" + ex.Message };
            }
        }
    }
}
