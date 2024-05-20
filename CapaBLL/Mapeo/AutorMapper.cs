using CapaBLL.Models;
using CapaINL.Autor.DTO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace CapaBLL.Mapeo
{
    public class AutorMapper
    {
        public static AutorModel EntidadToModel(CapaDAL.Entidad.Autor AutorEntidad)
        {
           AutorModel AutorModel = new AutorModel();
            AutorModel.IdAutor = AutorEntidad.IdAutor;
            AutorModel.Nombre = AutorEntidad.Nombre;
            AutorModel.FechaNacimiento = AutorEntidad.FechaNacimiento;
            AutorModel.Apellido = AutorEntidad.Apellido;

            return AutorModel;

        }
        public static CapaDAL.Entidad.Autor ModelToEntidad(AutorModel AutorModel)
        {
            CapaDAL.Entidad.Autor AutorEntidad = new CapaDAL.Entidad.Autor();
            AutorEntidad.IdAutor = AutorModel.IdAutor;
            AutorEntidad.Nombre = AutorModel.Nombre;
            AutorEntidad.FechaNacimiento = AutorModel.FechaNacimiento;
            AutorEntidad.Apellido = AutorModel.Apellido;

            return AutorEntidad;

        }
        public static AutorModel ResponseModel(Autor response)
        {
            if (response != null)
            {
                AutorModel autorModel = new AutorModel();
                autorModel.IdAutor = response.IdAutor;
                autorModel.Nombre = response.Nombre;
                autorModel.FechaNacimiento = Convert.ToDateTime(response.FechaNacimiento);
                autorModel.Apellido= response.Apellido;

                return autorModel;
            }
            return null;
        }

        public static Autor ModelResponse(AutorModel autorModel)
        {
            if (autorModel != null)
            {
                Autor AutorResponse = new Autor();
                AutorResponse.IdAutor = autorModel.IdAutor;
                AutorResponse.Nombre = autorModel.Nombre;
                AutorResponse.FechaNacimiento = Convert.ToDateTime(autorModel.FechaNacimiento);
                AutorResponse.Apellido = autorModel.Apellido;

                return AutorResponse;
            }
            return null;
        }
        
    }
}
