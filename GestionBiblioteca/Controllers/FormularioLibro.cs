using CapaBLL;
using CapaBLL.Models;
using GestionBiblioteca.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace GestionBiblioteca.Controllers
{
    public class FormularioLibro : Controller
    {
        private string url = null;
        private LibroServicio LibroServicio = new LibroServicio();
        private AutorServicio AutorServicio = new AutorServicio();
        public FormularioLibro()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            url = builder.GetSection("ApiSetting:endpointApis").Value;
        }


        // GET: FormularioLibro
        public ActionResult Index(string mensaje)
        {
            LibroViewModel viewModel = new LibroViewModel();
            viewModel.FechaPublicacion = DateTime.Today;

            // consumo de las apis. 
            viewModel.lista = LibroServicio.ConsultarLibrosApi(url);

            if(mensaje != null)
            {
                ModelState.AddModelError("",mensaje);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string consulta, LibroViewModel viewModel)
        {
            viewModel.lista = new List<LibroModel>();
            if (consulta.Equals("Adicionar"))
            {
                ModelState.Remove("id");
                bool datosValidos = true;
                if (viewModel.Nombre == null)
                {
                    ModelState.AddModelError("Nombre", "El Nombre es obligatorio");
                    datosValidos = false;
                }
                if (viewModel.FechaPublicacion.Year == 1)
                {
                    ModelState.AddModelError("FechaPublicacion", "La Fecha publicación es obligatorio");
                    datosValidos = false;
                }
                if (datosValidos)
                {
                    LibroModel model = new LibroModel();
                    model.Nombre = viewModel.Nombre;
                    model.FechaPublicacion = viewModel.FechaPublicacion;
                    int resultado = LibroServicio.AdicionarLibroApi(model, url);
                    if (resultado > 0)
                    {
                        ModelState.AddModelError("", "El Libro se creo correctamente");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al crear el Libro");
                    }
                    // consumo de las apis. 
                    viewModel.lista = LibroServicio.ConsultarLibrosApi(url);
                    viewModel.FechaPublicacion = new DateTime();
                    viewModel.Nombre = "";
                }
            }

            if (consulta.Equals("ConsultaID"))
            {
                ModelState.Remove("FechaPublicacion");
                ModelState.Remove("Nombre");

                if (viewModel.id == 0)
                {
                    ModelState.AddModelError("id", "El código es obligatorío para consultar");
                }
                else
                {
                    LibroModel Libro = LibroServicio.ConsultarLibroIDApi(viewModel.id, url);
                    if (Libro.Nombre == null)
                    {
                        ModelState.AddModelError("", "El libro ya no se encuentras disponible");
                    }
                    else
                    {
                        viewModel.lista.Add(Libro);
                        viewModel.id = 0;
                    }
                }
            }



            return View(viewModel);
        }

        public ActionResult EliminarLibro(int IdLibro)
        {
            LibroViewModel libroViewModel = new LibroViewModel();
            libroViewModel.id = IdLibro;

            LibroModel model = LibroServicio.ConsultarLibroIDApi(libroViewModel.id, url);
            libroViewModel.FechaPublicacion = model.FechaPublicacion;
            libroViewModel.Nombre = model.Nombre;
            return View(libroViewModel);
        }
        [HttpPost]
        public ActionResult EliminarLibro(string consulta, LibroViewModel libroViewModel)
        {
            if (consulta.Equals("Regresar"))
            {
                return RedirectToAction("Index");
            }
            if (consulta.Equals("Eliminar"))
            {
                //validar que no este asignado a un autor
                AutorLibroModel libroasignado = AutorServicio.ConsultarLibroAsignadoAPI(libroViewModel.id, url);
                if (libroasignado.Nombre == null)
                {
                    string respuesta =LibroServicio.EliminarLibroApi(libroViewModel.id, url);
                    if(respuesta != "" && respuesta.Contains("correctamente"))
                    {
                        return RedirectToAction("Index", new { mensaje = respuesta });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hubo un error al eliminar el libro");
                    }
                }
                else{
                    ModelState.AddModelError("","El libro no se puede eliminar ya que esta asigando a un autor.");
                }
            }

            return View(libroViewModel);
        }

        public ActionResult ModificarLibro(int IdLibro)
        {
            LibroViewModel libroViewModel = new LibroViewModel();
            libroViewModel.id = IdLibro;

            LibroModel model= LibroServicio.ConsultarLibroIDApi(libroViewModel.id, url);
            libroViewModel.FechaPublicacion = model.FechaPublicacion;
            libroViewModel.Nombre = model.Nombre;
            return View(libroViewModel);
        }

        [HttpPost]
        public ActionResult ModificarLibro(string consulta, LibroViewModel libroViewModel)
        {
            if (consulta.Equals("Regresar"))
            {
                return RedirectToAction("Index");
            }
            if (consulta.Equals("Modificar"))
            {
                bool datosValidos = true;
                if (libroViewModel.Nombre == null)
                {
                    ModelState.AddModelError("Nombre", "El Nombre es obligatorio");
                    datosValidos = false;
                }
                if (libroViewModel.FechaPublicacion.Year == 1)
                {
                    ModelState.AddModelError("FechaPublicacion", "La Fecha publicación es obligatorio");
                    datosValidos = false;
                }
                if (datosValidos)
                {
                    try
                    {
                        LibroModel model = new LibroModel();
                        model.Nombre = libroViewModel.Nombre;
                        model.FechaPublicacion = libroViewModel.FechaPublicacion;
                        model.IdLibro = libroViewModel.id;
                        string resultado = LibroServicio.ModificarLibroApi(model, url);
                        if (resultado != "" && resultado.Contains("correctamente"))
                        {
                            return RedirectToAction("Index", new { mensaje = resultado });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error al modificar el libro");
                        }
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("",ex.Message);
                    }
                }
            }

            return View(libroViewModel);
        }
    }
}
