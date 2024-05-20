using System.ComponentModel.DataAnnotations;

namespace CapaDAL.Entidad
{
    public class Libro
    {
        [Key] public int IdLibro { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
