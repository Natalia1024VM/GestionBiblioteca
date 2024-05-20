using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapaINL.Autor.DTO
{
    public partial class Autor
    {
        [JsonPropertyName("idAutor")]
        public int IdAutor { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }

    //public partial class Autor
    //{
    //    public static List<Autor> FromJson(string json) => JsonConvert.DeserializeObject<List<Autor>>(json,CapaINL.Autor.DTO.Converter.Settings);
    //}
    //internal static class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters =
    //        {
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}
}
