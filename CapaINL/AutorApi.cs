using CapaINL.Autor.DTO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CapaINL
{
    public class AutorApi
    {

        public async Task<List<Autor.DTO.Autor>> ConsultarLista(string Endpoint)
        {
            List<Autor.DTO.Autor> respuesta = new List<Autor.DTO.Autor>();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.GetAsync("api/Autor/");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<List<CapaINL.Autor.DTO.Autor>>(json_respuesta);
            }

            return respuesta;
        }

        public async Task<Autor.DTO.Autor> ConsultarAutor(Autor.DTO.Autor request, string Endpoint) {
            Autor.DTO.Autor respuesta = new Autor.DTO.Autor();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync($"api/Autor/PorNombre/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<CapaINL.Autor.DTO.Autor>(json_respuesta);
            }

            return respuesta;
        }
        public async Task<Autor.DTO.Autor> ConsultarAutorID(int id, string Endpoint)
        {
            Autor.DTO.Autor respuesta = new Autor.DTO.Autor();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.GetAsync($"api/Autor/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<CapaINL.Autor.DTO.Autor>(json_respuesta);
            }

            return respuesta;
        }

        public async Task<int> AdicionarAutor(Autor.DTO.Autor autorRequest, string Endpoint)
        {
            int resultado = 0;

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(autorRequest), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Autor/", content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = JsonConvert.DeserializeObject<int>(json_respuesta);
            }

            return resultado;
        }

        public async Task<string> ModificarAutor(Autor.DTO.Autor autorRequest, string Endpoint)
        {
            string resultado = "";
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(autorRequest), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Autor/{autorRequest.IdAutor}", content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = json_respuesta;
            }

            return resultado;
        }

        public async Task<string> EliminarAutor(int idAutor, string Endpoint)
        {
            string resultado = "";
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.DeleteAsync($"api/Autor/{idAutor}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = json_respuesta;
            }

            return resultado;
        }

        public async Task<string> AdicionarLibroAutor(AutorLibroRequest AutorLibroRequest, string Endpoint)
        {
            string resultado = "";
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(AutorLibroRequest), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Autor/AdicionarLibroAutor",content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = json_respuesta;
            }

            return resultado;
        }

        
        public async Task<List<Autor.DTO.AutorLibroResponse>> ConsultarLibrosAutor(int idAutor, string Endpoint)
        {
            List<Autor.DTO.AutorLibroResponse> respuesta = new List<Autor.DTO.AutorLibroResponse>();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.GetAsync($"api/Autor/ConsultarLibrosAutor/{idAutor}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<List<CapaINL.Autor.DTO.AutorLibroResponse>>(json_respuesta);
            }

            return respuesta;
        }

        public async Task<Autor.DTO.AutorLibroResponse> ConsultarLibroAsignadoAPI(int idAutor, string Endpoint)
        {
            Autor.DTO.AutorLibroResponse respuesta = new Autor.DTO.AutorLibroResponse();
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.GetAsync($"api/Autor/ConsultarLibroAsignado/{idAutor}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<CapaINL.Autor.DTO.AutorLibroResponse>(json_respuesta);
            }

            return respuesta;
        }

        public async Task<string> EliminarLibroAutorApi(int idAutor,int idLibro, string Endpoint)
        {
            string resultado = "";
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(Endpoint);
            var response = await cliente.DeleteAsync($"api/Autor/EliminarLibroAutor/{idAutor}/{idLibro}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                resultado = json_respuesta;
            }

            return resultado;
        }

    }
}
