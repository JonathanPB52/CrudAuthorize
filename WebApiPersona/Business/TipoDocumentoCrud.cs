using WebApiPersona.DataAccess;
using WebApiPersona.Models.Contracts;
using WebApiPersona.Models.Data;

namespace WebApiPersona.Business
{
    public class TipoDocumentoCrud : ICrudTarjetaIdentidad
    {
        private readonly IConfiguration _configuration;
        public TipoDocumentoCrud(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<TipoDocumento> ConsultarTipoDocumentos() {
            List<TipoDocumento> lista = new List<TipoDocumento>();
            using (CrudSpContext crudSpContext= new CrudSpContext(_configuration))
            {
                lista = crudSpContext.TipoDocumentos.ToList();
            }
            return lista;   
        }
    }
}
