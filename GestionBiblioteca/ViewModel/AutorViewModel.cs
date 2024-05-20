using CapaBLL.Models;
using System.ComponentModel;

namespace GestionBiblioteca.ViewModel
{
    public class AutorViewModel
    {
        public List<AutorModel> lista { get; set; }
        [DisplayName("Nombre del Autor a buscar")]
        public string NombreBuscar { get; set; }

        [DisplayName("Nombre Autor")]
        public string Nombre { get; set; }
        
        [DisplayName("Apellidos Autor")]
        public string Apellido { get; set; }
        
        [DisplayName("Fecha Nacimiento Autor")]
        public DateTime FechaNacimiento {  get; set; }

        [DisplayName("Identificación del Autor")]
        public int id { get; set; }
        [DisplayName("Libro")]
        public int IdLibro { get; set; }
        public List<AutorLibroModel> ListaLibros { get; set; }
        public string NombreLibro { get; set; }


    }
}
