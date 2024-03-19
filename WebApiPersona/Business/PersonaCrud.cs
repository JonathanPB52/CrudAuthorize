using System.ComponentModel;
using WebApiPersona.DataAccess;
using WebApiPersona.Models.Contracts;
using WebApiPersona.Models.Data;

namespace WebApiPersona.Business
{
    public class PersonaCrud : ICrudPersona
    {
        private readonly IConfiguration _configuration;
        public PersonaCrud(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string ICrudPersona.ActualizarPersona(int id, Persona obj)
        {
            try
            {

                using (CrudSpContext crudSpContext = new CrudSpContext(_configuration))
                {
                    var persona = crudSpContext.Personas.Where(x => x.Id == id && x.Estado > 0).ToList();
                    Persona personaM = new Persona();
                    personaM = persona[0] as Persona;
                    if (personaM != null)
                    {
                        personaM.Id = obj.Id;
                        personaM.PrimerNombre = obj.PrimerNombre.Trim();
                        personaM.SegundoNombre = obj.SegundoNombre.Trim();
                        personaM.PrimerApeliido = obj.PrimerApeliido.Trim();
                        personaM.SegundoApellido = obj.SegundoApellido.Trim();
                        personaM.FechaNacimiento = obj.FechaNacimiento;
                        personaM.Documento = obj.Documento;
                        personaM.IdTipoDocumento = obj.IdTipoDocumento;
                        personaM.Estado = obj.Estado;
                        crudSpContext.Update(personaM);
                        crudSpContext.SaveChanges();
                    }
                    else
                    {
                        return "No se encontro registro, validar el estado y el id";
                    }
                }
                return "Persona editada exitosamente";

            }
            catch (Exception ex)
            {
                return "Ocurrio un error buissness al intentar editar la persona." + ex.Message;
            }
        }

        List<Persona> ICrudPersona.ConsultarPersona()
        {
            List<Persona> list = new List<Persona>();
            using (CrudSpContext crudSpContext = new CrudSpContext(_configuration))
            //list = crudSpContext.Personas.Where(x => x.Estado == 1).ToList();
            list = crudSpContext.Personas.Where(x => x.Estado > 0).ToList();

            foreach (var item in list)
            {
                item.PrimerNombre = item.PrimerNombre.Trim();
                item.SegundoNombre = item.SegundoNombre.Trim();
                item.PrimerApeliido = item.PrimerApeliido.Trim();
                item.SegundoApellido = item.SegundoApellido.Trim();
            }
            return list;
        }

        string ICrudPersona.CrearPersona(Persona obj)
        {
            Persona persona = new Persona();
            persona = obj;
            try
            {
                using (CrudSpContext crudSpContext = new CrudSpContext(_configuration))
                {
                    crudSpContext.Personas.Add(persona);
                    crudSpContext.SaveChanges();
                }
                return "Persona registrada exitosamente";

            }
            catch (Exception ex)
            {
                return "Ocurrio un error buissness al intentar registrar la persona." + ex.Message;
            }
        }

        string ICrudPersona.EliminarPersona(int id)
        {
            try
            {
                using (CrudSpContext crudSpContext = new CrudSpContext(_configuration))
                {
                    var persona = crudSpContext.Personas.Where(x => x.Id == id).ToList();
                    Persona personaM = new Persona();
                    personaM = persona[0] as Persona;
                    if (personaM != null)
                    {
                        personaM.Estado = 0;
                        crudSpContext.Update(personaM);
                        crudSpContext.SaveChanges();
                    }
                    else
                    {
                        return "No se encontro registro";
                    }
                }
                return "Persona eliminada exitosamente";

            }
            catch (Exception ex)
            {
                return "Ocurrio un error buissness al intentar eliminar la persona." + ex.Message;
            }
        }
    }
}
