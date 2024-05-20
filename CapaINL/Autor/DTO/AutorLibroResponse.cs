using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaINL.Autor.DTO
{
    public class AutorLibroResponse
    {
        public int IdLibro { get; set; }
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
