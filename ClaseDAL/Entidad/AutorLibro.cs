using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL.Entidad
{
    public class AutorLibro
    {
        [Key] public int IdLibro { get; set; }
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
