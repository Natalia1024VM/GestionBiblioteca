using CapaBLL.Mapeo;
using CapaBLL.Models;
using CapaDAL;
using CapaDAL.Entidad;
using CapaINL;
using CapaINL.Autor.DTO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace CapaBLL
{
    public class AutorServicio
    {
        private AutorApi autorApi = new AutorApi();

        public int AgregarAutor(AutorModel Autor, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();

            var id = AutorRepositorio.AgregarAutor(AutorMapper.ModelToEntidad(Autor), application);

            return id;
        }

        public AutorModel ConsultarAutorID(int id, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();
            var resultado =  AutorRepositorio.ConsultarAutorIDAsync(id, application);

            if (resultado.Result != null)
            {
                AutorModel AutorModel = AutorMapper.EntidadToModel(resultado.Result);
                return AutorModel;
            }
            else
            {
                return null;
            }
        }

        public AutorModel ConsultarAutor(string nombre, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();
            var resultado = AutorRepositorio.ConsultarAutorAsync(nombre, application);

            if (resultado.Result != null)
            {
                AutorModel AutorModel = AutorMapper.EntidadToModel(resultado.Result);
                return AutorModel;
            }
            else
            {
                return null;
            }
        }
        public List<AutorModel> ConsultarAutors(ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();
            var resultado = AutorRepositorio.ConsultarAutors(application);
            if (resultado.Result != null)
            {
                List<AutorModel> lista = new List<AutorModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(AutorMapper.EntidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }
        public bool ModificarAutor(int id, AutorModel AutorModel, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();

            int resultado = AutorRepositorio.ModificarAutor(id, AutorMapper.ModelToEntidad(AutorModel), application);
            if(resultado > 0)
            {
                return true;
            }
            else
            {
               return false;
            }
        }
        public bool EliminarAutor(int id,ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();

            int resultado = AutorRepositorio.EliminarAutor(id, application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AdicionarLibroAutor(int idAutor, int idLibro, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();

            int resultado = AutorRepositorio.AdicionarLibroAutor(idAutor, idLibro, application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarLibroAutor(int idAutor, int idLibro, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();

            int resultado = AutorRepositorio.EliminarLibroAutor(idAutor, idLibro, application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AutorLibroModel> ConsultarLibrosAutor(int idAutor, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();
            List<AutorLibroModel> lista = new List<AutorLibroModel>();
            var resultado = AutorRepositorio.ConsultarLibrosAutor(idAutor, application);
            if (resultado.Result != null)
            {
                foreach (var item in resultado.Result)
                {
                    lista.Add(AutorLibroMapper.EntidadModel(item));
                }
            }
            return lista;

        }

        public AutorLibroModel ConsultarLibroAsignado(int idLibro, ApplicationDbContext application)
        {
            AutorRepositorio AutorRepositorio = new AutorRepositorio();
            var resultado = AutorRepositorio.ConsultarLibroAsignado(idLibro, application);

            if (resultado.Result != null)
            {
                AutorLibroModel AutorModel = AutorLibroMapper.EntidadModel(resultado.Result);
                return AutorModel;
            }
            else
            {
                return null;
            }
        }


        // Metodos para consultar las APIS
        public List<AutorModel> ConsultarAutoresApi(string endpoint)
        {
            List<AutorModel> lista = new List<AutorModel>();
            var resultado = autorApi.ConsultarLista(endpoint);
            if (resultado.Result != null)
            {
                foreach (var item in resultado.Result)
                {
                    lista.Add(AutorMapper.ResponseModel(item));
                }
            }
            return lista;

        }
        public AutorModel ConsultarAutorNombreApi(AutorModel model, string endpoint)
        {
            AutorModel autorModel = new AutorModel();
            var resultado = autorApi.ConsultarAutor(AutorMapper.ModelResponse(model), endpoint);
            if (resultado.Result != null)
            {
                autorModel = AutorMapper.ResponseModel(resultado.Result);
            }
            return autorModel;

        }
        public AutorModel ConsultarAutorIDApi(int id, string endpoint)
        {
            AutorModel autorModel = new AutorModel();
            var resultado = autorApi.ConsultarAutorID(id, endpoint);
            if (resultado.Result != null)
            {
                autorModel = AutorMapper.ResponseModel(resultado.Result);
            }
            return autorModel;

        }
        public int AdicionarAutorApi(AutorModel autorModel, string endpoint)
        {
            int resultado = 0;
            var response =autorApi.AdicionarAutor(AutorMapper.ModelResponse(autorModel), endpoint);

            if(response.Result >0 )
            {
                resultado = response.Result;
            }
            return resultado;

        }

        public string ModificarAutorApi(AutorModel autorModel, string endpoint)
        {
            string resultado = "";
            var response = autorApi.ModificarAutor(AutorMapper.ModelResponse(autorModel), endpoint);
            resultado = response.Result;

            return resultado;
        }

        public string EliminarAutorApi(int idAutor, string endpoint)
        {
            string resultado = "";
            var response = autorApi.EliminarAutor(idAutor, endpoint);
            resultado = response.Result;

            return resultado;
        }

        public string AdicionarLibroAutorApi(AutorLibroModel request, string endpoint)
        {
            string resultado = "";
            var response = autorApi.AdicionarLibroAutor(AutorLibroMapper.ModelRequest(request), endpoint);
            resultado = response.Result;

            return resultado;
        }

        public List<AutorLibroModel> ConsultarLibrosAutorAPI(int idAutor, string endpoint)
        {
            List<AutorLibroModel> lista = new List<AutorLibroModel>();
            var resultado = autorApi.ConsultarLibrosAutor(idAutor, endpoint);
            if (resultado.Result != null)
            {
                foreach (var item in resultado.Result)
                {
                    lista.Add(AutorLibroMapper.ResponseModel(item));
                }
            }
            return lista;

        }

        public AutorLibroModel ConsultarLibroAsignadoAPI(int idLibro, string endpoint)
        {
            AutorLibroModel model = new AutorLibroModel();
            var resultado = autorApi.ConsultarLibroAsignadoAPI(idLibro, endpoint);
            if (resultado.Result != null)
            {
                model = AutorLibroMapper.ResponseModel(resultado.Result);
            }
            return model;

        }

        public string EliminarLibroAutorApi(int idAutor, int idLibro, string endpoint)
        {
            string resultado = "";
            var response = autorApi.EliminarLibroAutorApi(idAutor,idLibro, endpoint);
            resultado = response.Result;

            return resultado;
        }

    }
}
