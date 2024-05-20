using CapaBLL;
using CapaBLL.Models;
using CapaDAL;
using CapaINL.Autor.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestionBiblioteca.Servicios
{
    [ApiController]
    [Route("api/Autor/")]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext conection;
        AutorServicio autorServicio = new AutorServicio();
        public AutorController(ApplicationDbContext conection)
        {
            this.conection = conection;
        }

        [HttpGet]
        public IEnumerable<AutorModel> Get()
        {
            List<AutorModel> AutorsModel = autorServicio.ConsultarAutors(conection);

            return AutorsModel;
        }

        [HttpPost]
        public int Post(AutorModel Autor)
        {
            var id = autorServicio.AgregarAutor(Autor, conection);
            return id;
        }

        [HttpGet("{idAutor:int}")]
        public AutorModel Get(int idAutor)
        {
            AutorModel AutorModel = autorServicio.ConsultarAutorID(idAutor, conection);

            return AutorModel;
        }

        [HttpPost("PorNombre")]
        public AutorModel PostAutor(AutorModel model)
        {
            AutorModel AutorModel = autorServicio.ConsultarAutor(model.Nombre, conection);

            return AutorModel;
        }

        [HttpPut("{idAutor:int}")]
        public string actualiza(int idAutor, AutorModel Autor)
        {
            bool AutorModel = autorServicio.ModificarAutor(idAutor, Autor, conection);
            if (AutorModel)
            {
                return "El Autor se modifico correctamente";
            }
            else
            {
                return "Error al modificar el Autor";
            }
        }

        [HttpDelete("{id:int}")]
        public string elimina(int id)
        {
            bool AutorModel = autorServicio.EliminarAutor(id, conection);
            if (AutorModel)
            {
                return "El Autor se elimino correctamente";
            }
            else
            {
                return "Error al eliminar el Autor";
            }
        }

        [HttpPut("AdicionarLibroAutor")]
        public string AdicionarLibroAutor(AutorLibroModel autorLibroMode)
        {
            bool id = autorServicio.AdicionarLibroAutor(autorLibroMode.IdAutor, autorLibroMode.IdLibro, conection);
            if (id)
            {
                return "El libro se adiciono correctamente";
            }
            else
            {
                return "Error al adicionar el libro";
            }

        }

        [HttpDelete("EliminarLibroAutor/{idAutor:int}/{idLibro:int}")]
        public string EliminarLibroAutor(int idAutor, int idLibro)
        {
            bool id = autorServicio.EliminarLibroAutor(idAutor, idLibro, conection);
            if (id)
            {
                return "El libro se elimino correctamente";
            }
            else
            {
                return "Error al eliminar el libro";
            }

        }


        [HttpGet("ConsultarLibrosAutor/{idAutor:int}")]
        public IEnumerable<AutorLibroModel> ConsultarLibrosAutor(int idAutor)
        {
            List<AutorLibroModel> lista = autorServicio.ConsultarLibrosAutor(idAutor, conection);

            return lista;
        }

        [HttpGet("ConsultarLibroAsignado/{idlibro:int}")]
        public AutorLibroModel ConsultarLibroAsignado(int idlibro)
        {
            AutorLibroModel lista = autorServicio.ConsultarLibroAsignado(idlibro, conection);

            return lista;
        }


    }
}
