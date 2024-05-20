using CapaBLL;
using CapaBLL.Models;
using CapaDAL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestionBiblioteca.Servicios
{
    [ApiController]
    [Route("api/Libro")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext conection;
        LibroServicio libroServicio = new LibroServicio();

        public LibroController(ApplicationDbContext conection)
        {
            this.conection = conection;
        }

        [HttpGet]
        public IEnumerable<LibroModel> Get()
        {
            List<LibroModel> librosModel = libroServicio.ConsultarLibros(conection);

            return librosModel;
        }

        [HttpPost]
        public int Post(LibroModel libro)
        {
            var id = libroServicio.AgregarLibro(libro, conection);
            return id;
        }

        [HttpGet("{id:int}")]
        public LibroModel Get(int id)
        {
            LibroModel libroModel = libroServicio.ConsultarLibroID(id, conection);

            return libroModel;
        }

        [HttpPut("{id:int}")]
        public string Put(int id, LibroModel libro)
        {
            bool libroModel = libroServicio.ModificarLibro(id, libro, conection);
            if (libroModel)
            {
                return "El libro se modifico correctamente";
            }
            else
            {
                return "Error al modificar el libro";
            }
        }

        [HttpDelete("{id:int}")]
        public string Delete(int id)
        {
            bool libroModel = libroServicio.EliminarLibro(id, conection);
            if (libroModel)
            {
                return "El libro se elimino correctamente";
            }
            else
            {
                return "Error al eliminar el libro";
            }
        }




    }
}
