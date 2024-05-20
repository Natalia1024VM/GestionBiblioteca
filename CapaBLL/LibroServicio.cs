using CapaBLL.Mapeo;
using CapaBLL.Models;
using CapaDAL;
using CapaINL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace CapaBLL
{
    public class LibroServicio
    {
        private LibroApi LibroApi = new LibroApi();

        public int AgregarLibro(LibroModel libro, ApplicationDbContext application)
        {
            LibroRepositorio LibroRepositorio = new LibroRepositorio();

            var id = LibroRepositorio.AgregarLibro(LibroMapper.ModelToEntidad(libro), application);

            return id;
        }

        public LibroModel ConsultarLibroID(int id, ApplicationDbContext application)
        {
            LibroRepositorio LibroRepositorio = new LibroRepositorio();
            var resultado =  LibroRepositorio.ConsultarLibroIDAsync(id, application);

            if (resultado.Result != null)
            {
                LibroModel LibroModel = LibroMapper.EntidadToModel(resultado.Result);
                return LibroModel;
            }
            else
            {
                return null;
            }
        }

        public List<LibroModel> ConsultarLibros(ApplicationDbContext application)
        {
            LibroRepositorio LibroRepositorio = new LibroRepositorio();
            var resultado = LibroRepositorio.ConsultarLibros(application);
            if (resultado.Result != null)
            {
                List<LibroModel> lista = new List<LibroModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(LibroMapper.EntidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }
        public bool ModificarLibro(int id, LibroModel libroModel, ApplicationDbContext application)
        {
            LibroRepositorio LibroRepositorio = new LibroRepositorio();

            int resultado = LibroRepositorio.ModificarLibro(id, LibroMapper.ModelToEntidad(libroModel), application);
            if(resultado > 0)
            {
                return true;
            }
            else
            {
               return false;
            }
        }
        public bool EliminarLibro(int id,ApplicationDbContext application)
        {
            LibroRepositorio LibroRepositorio = new LibroRepositorio();

            int resultado = LibroRepositorio.EliminarLibro(id, application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Metodos para consultar las APIS
        public List<LibroModel> ConsultarLibrosApi(string endpoint)
        {
            List<LibroModel> lista = new List<LibroModel>();
            var resultado = LibroApi.ConsultarLista(endpoint);
            if (resultado.Result != null)
            {
                foreach (var item in resultado.Result)
                {
                    lista.Add(LibroMapper.ResponseModel(item));
                }
            }
            return lista;

        }
        public LibroModel ConsultarLibroIDApi(int idLibro, string endpoint)
        {
            LibroModel LibroModel = new LibroModel();
            var resultado = LibroApi.ConsultarLibro(idLibro, endpoint);
            if (resultado.Result != null)
            {
                LibroModel = LibroMapper.ResponseModel(resultado.Result);
            }
            return LibroModel;

        }
        public int AdicionarLibroApi(LibroModel LibroModel, string endpoint)
        {
            int resultado = 0;
            var response = LibroApi.AdicionarLibro(LibroMapper.ModelResponse(LibroModel), endpoint);

            if (response.Result > 0)
            {
                resultado = response.Result;
            }
            return resultado;

        }

        public string ModificarLibroApi(LibroModel LibroModel, string endpoint)
        {
            string resultado = "";
            var response = LibroApi.ModificarLibro(LibroMapper.ModelResponse(LibroModel), endpoint);
            resultado = response.Result;

            return resultado;
        }
    
        public string EliminarLibroApi(int idLibro, string endpoint)
        {
            string resultado = "";
            var response = LibroApi.EliminarLibro(idLibro, endpoint);
            resultado = response.Result;

            return resultado;
        }
    
    }
}
