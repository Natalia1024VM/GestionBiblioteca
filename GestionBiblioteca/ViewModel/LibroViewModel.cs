using CapaBLL.Models;
using System.ComponentModel;

namespace GestionBiblioteca.ViewModel
{
    public class LibroViewModel
    {
        public List<LibroModel> lista { get; set; }
        [DisplayName("Nombre Libro")]

        public string Nombre { get; set; }
        
        [DisplayName("Fecha Publicación Libro")]
        public DateTime FechaPublicacion { get; set; }

        [DisplayName("Código Libro")]
        public int id { get; set; }
    }
}
