using System.ComponentModel.DataAnnotations;

namespace CapaDAL.Entidad
{
    public class Autor
    {
        [Key] public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
