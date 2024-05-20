using CapaBLL.Models;
using CapaDAL.Entidad;
using CapaINL.Autor.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBLL.Mapeo
{
    public class AutorLibroMapper
    {
        public static AutorLibroRequest ModelRequest(AutorLibroModel Model)
        {
            if (Model != null)
            {
                AutorLibroRequest request = new AutorLibroRequest();
                request.IdLibro = Model.IdLibro;
                request.IdAutor = Model.IdAutor;

                return request;
            }
            return null;
        }

        public static AutorLibroModel EntidadModel(AutorLibro request)
        {
            if (request != null)
            {
                AutorLibroModel model = new AutorLibroModel();
                model.IdAutor = request.IdAutor;
                model.IdLibro = request.IdLibro;
                model.Nombre = request.Nombre;
                model.FechaPublicacion = request.FechaPublicacion;
                return model;
            }
            return null;
        }
        public static AutorLibroModel ResponseModel(AutorLibroResponse response)
        {
            if (response != null)
            {
                AutorLibroModel model = new AutorLibroModel();
                model.IdAutor = response.IdAutor;
                model.IdLibro = response.IdLibro;
                model.Nombre = response.Nombre;
                model.FechaPublicacion = response.FechaPublicacion;
                return model;
            }
            return null;
        }
    }
}
