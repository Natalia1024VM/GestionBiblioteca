using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaINL
{
    public class LibroApi
    {
        public async Task<List<Libro.DTO.Libro>> ConsultarLista(string Endpoint)
        {
            List<Libro.DTO.Libro> respuesta = new List<Libro.DTO.Libro>();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.GetAsync("api/Libro/");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<List<CapaINL.Libro.DTO.Libro>>(json_respuesta);
            }

            return respuesta;
        }

        public async Task<Libro.DTO.Libro> ConsultarLibro(int IdLibro, string Endpoint)
        {
            Libro.DTO.Libro respuesta = new Libro.DTO.Libro();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.GetAsync($"api/Libro/{IdLibro}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<CapaINL.Libro.DTO.Libro>(json_respuesta);
            }

            return respuesta;
        }

        public async Task<int> AdicionarLibro(Libro.DTO.Libro LibroRequest, string Endpoint)
        {
            int resultado = 0;

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(LibroRequest), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Libro/", content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = JsonConvert.DeserializeObject<int>(json_respuesta);
            }

            return resultado;
        }

        public async Task<string> ModificarLibro(Libro.DTO.Libro LibroRequest, string Endpoint)
        {
            string resultado = "";
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(LibroRequest), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Libro/{LibroRequest.IdLibro}", content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = json_respuesta;
            }

            return resultado;
        }

        public async Task<string> EliminarLibro(int idLibro, string Endpoint)
        {
            string resultado = "";
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.DeleteAsync($"api/Libro/{idLibro}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = json_respuesta;
            }

            return resultado;
        }
    }
}

