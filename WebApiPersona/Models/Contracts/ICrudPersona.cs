using WebApiPersona.Models.Data;

namespace WebApiPersona.Models.Contracts
{
    public interface ICrudPersona
    {

        public string CrearPersona(Persona obj);

        public List<Persona> ConsultarPersona();

        public string ActualizarPersona(int id, Persona obj);

        public string EliminarPersona(int id);

    }
}
