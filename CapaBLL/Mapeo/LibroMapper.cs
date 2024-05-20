using CapaBLL.Models;
using CapaINL.Autor.DTO;
using CapaINL.Libro.DTO;

namespace CapaBLL.Mapeo
{
    public class LibroMapper
    {
        public static LibroModel EntidadToModel(CapaDAL.Entidad.Libro libroEntidad)
        {
            LibroModel LibroModel = new LibroModel();
            LibroModel.IdLibro = libroEntidad.IdLibro;
            LibroModel.Nombre = libroEntidad.Nombre;
            LibroModel.FechaPublicacion = libroEntidad.FechaPublicacion;

            return LibroModel;

        }
        public static CapaDAL.Entidad.Libro ModelToEntidad(LibroModel LibroModel)
        {
            CapaDAL.Entidad.Libro libroEntidad = new CapaDAL.Entidad.Libro();
            libroEntidad.IdLibro = LibroModel.IdLibro;
            libroEntidad.Nombre = LibroModel.Nombre;
            libroEntidad.FechaPublicacion = LibroModel.FechaPublicacion;

            return libroEntidad;

        }
        public static LibroModel ResponseModel(Libro response)
        {
            if (response != null)
            {
                LibroModel LibroModel = new LibroModel();
                LibroModel.IdLibro = response.IdLibro;
                LibroModel.Nombre = response.Nombre;
                LibroModel.FechaPublicacion = Convert.ToDateTime(response.FechaPublicacion);

                return LibroModel;
            }
            return null;
        }

        public static Libro ModelResponse(LibroModel LibroModel)
        {
            if (LibroModel != null)
            {
                Libro LibroResponse = new Libro();
                LibroResponse.IdLibro = LibroModel.IdLibro;
                LibroResponse.Nombre = LibroModel.Nombre;
                LibroResponse.FechaPublicacion = Convert.ToDateTime(LibroModel.FechaPublicacion);

                return LibroResponse;
            }
            return null;
        }
      



    }
}
