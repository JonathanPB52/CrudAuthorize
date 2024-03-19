using WebApiPersona.Models.Data;

namespace WebApiPersona.Models.Contracts
{
    public interface ICrudTarjetaIdentidad
    {
        public List<TipoDocumento> ConsultarTipoDocumentos();
    }
}
