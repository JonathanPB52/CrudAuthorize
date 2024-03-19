using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiPersona.Models.Contracts;
using WebApiPersona.Models.Data;

namespace WebApiPersona.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ICrudTarjetaIdentidad _crudTarjetaIdentidad;
        public TipoDocumentoController(ICrudTarjetaIdentidad crudTarjetaIdentidad)
        {
            _crudTarjetaIdentidad = crudTarjetaIdentidad;
        }

        [HttpGet]
        [Route("ConsultarTipoDocumento")]
        public dynamic GetTipoDocumentos()
        {
            try
            {
                List<TipoDocumento> lis = _crudTarjetaIdentidad.ConsultarTipoDocumentos();
                foreach (var item in lis)
                {
                    item.Tdocumento = item.Tdocumento.Trim();
                }
                return lis;

            }catch(Exception ex)
            {
                return new { success = true, mesagge = "Error servicio consultar tipo documento" + ex.Message };
            }
        }
    }
}
