using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapaINL.Libro.DTO
{
    public class Libro
    {
        [JsonPropertyName("idLibro")]
        public int IdLibro { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("fechaPublicacion")]
        public DateTime FechaPublicacion { get; set; }
    }
}
