using CapaBLL;
using CapaBLL.Models;
using CapaINL;
using GestionBiblioteca.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace GestionBiblioteca.Controllers
{
    public class FormularioAutor : Controller
    {
        private string url = null;
        private AutorServicio AutorServicio = new AutorServicio();
        private LibroServicio LibroServicio= new LibroServicio();
        public FormularioAutor() {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            url = builder.GetSection("ApiSetting:endpointApis").Value;
        }

        // GET: FormularioAutor
        public ActionResult Index(string mensaje)
        {
            AutorViewModel viewModel = new AutorViewModel();
            viewModel.FechaNacimiento = DateTime.Today;
            // consumo de las apis. 
            viewModel.lista = AutorServicio.ConsultarAutoresApi(url);

            if(mensaje != null)
            {
                ModelState.AddModelError("",mensaje);
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(string consulta, AutorViewModel viewModel)
        {
            viewModel.lista = new List<AutorModel>();
            if (consulta.Equals("Adicionar"))
            {
                ModelState.Remove("id");
                ModelState.Remove("NombreBuscar");
                bool datosValidos = true;
                if(viewModel.Apellido == null)
                {
                    ModelState.AddModelError("Apellido", "El Apellido es obligatorio");
                    datosValidos = false;
                }
                if (viewModel.Nombre == null)
                {
                    ModelState.AddModelError("Nombre", "El Nombre es obligatorio");
                    datosValidos = false;
                }
                if (viewModel.FechaNacimiento.Year ==1)
                {
                    ModelState.AddModelError("FechaNacimiento", "La Fecha Nacimiento es obligatorio");
                    datosValidos = false;
                }
                if (datosValidos)
                {
                    AutorModel model = new AutorModel();
                    model.Nombre = viewModel.Nombre;
                    model.FechaNacimiento = viewModel.FechaNacimiento;
                    model.Apellido = viewModel.Apellido;
                    int resultado = AutorServicio.AdicionarAutorApi(model, url);
                    if (resultado > 0)
                    {
                        ModelState.AddModelError("", "El autor se creo correctamente");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al crear el autor");
                    }
                    // consumo de las apis. 
                    viewModel.lista = AutorServicio.ConsultarAutoresApi(url);
                    viewModel.FechaNacimiento = new DateTime();
                    viewModel.Nombre = "";
                    viewModel.Apellido = "";
                }
            }
            
            if (consulta.Equals("ConsultaID"))
            {
                ModelState.Remove("FechaNacimiento");
                ModelState.Remove("Apellido");
                ModelState.Remove("Nombre");

                if (viewModel.NombreBuscar== "")
                {
                    ModelState.AddModelError("NombreBuscar", "El dato es obligatorío para consultar");

                }
                else
                {
                    AutorModel modelBuscar = new AutorModel();
                    modelBuscar.IdAutor = 0;
                    modelBuscar.FechaNacimiento = DateTime.Now;
                    modelBuscar.Nombre = viewModel.NombreBuscar;
                    modelBuscar.Apellido = " ";
                    AutorModel autor= AutorServicio.ConsultarAutorNombreApi(modelBuscar, url);
                    if (autor.Nombre == null)
                    {
                        ModelState.AddModelError("", "No existe ningun autor con ese nombre");
                    }
                    else
                    {
                        viewModel.lista.Add(autor);
                        viewModel.id = 0;
                    }
                }
            }

            

            return View(viewModel);
        }

        public ActionResult EliminarAutor(int IdAutor)
        {
            AutorViewModel autorViewModel = new AutorViewModel();
            autorViewModel.id = IdAutor;
            //Conusltar el autor 
            AutorModel autorModel = AutorServicio.ConsultarAutorIDApi(IdAutor, url);

            autorViewModel.Apellido = autorModel.Apellido;
            autorViewModel.Nombre = autorModel.Nombre;
            autorViewModel.FechaNacimiento = autorModel.FechaNacimiento;

            return View(autorViewModel);
        }
        [HttpPost]
        public ActionResult EliminarAutor(string consulta, AutorViewModel autorViewModel)
        {
            if (consulta.Equals("Regresar"))
            {
                return RedirectToAction("Index");
            }
            if (consulta.Equals("Eliminar"))
            {

                try { 
                string resultado = AutorServicio.EliminarAutorApi(autorViewModel.id, url);
                if (resultado != "")
                {
                    return RedirectToAction("Index", new { mensaje = resultado });
                }
                else
                {
                    ModelState.AddModelError("", "Error al eliminar el autor");
                }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(autorViewModel);
        }

        public ActionResult ModificarAutor(int IdAutor)
        {
            AutorViewModel autorViewModel = new AutorViewModel();
            autorViewModel.id = IdAutor;

            //Conusltar el autor 
            AutorModel autorModel= AutorServicio.ConsultarAutorIDApi(IdAutor, url);

            autorViewModel.Apellido = autorModel.Apellido;
            autorViewModel.Nombre = autorModel.Nombre;
            autorViewModel.FechaNacimiento = autorModel.FechaNacimiento;

            return View(autorViewModel);
        }

        [HttpPost]
        public ActionResult ModificarAutor(string consulta, AutorViewModel autorViewModel)
        {
            if (consulta.Equals("Regresar"))
            {
                return RedirectToAction("Index");
            }
            if (consulta.Equals("Modificar"))
            {
                try
                {
                    bool datosValidos = true;
                    if (autorViewModel.Apellido == null)
                    {
                        ModelState.AddModelError("Apellido", "El Apellido es obligatorio");
                        datosValidos = false;
                    }
                    if (autorViewModel.Nombre == null)
                    {
                        ModelState.AddModelError("Nombre", "El Nombre es obligatorio");
                        datosValidos = false;
                    }
                    if (autorViewModel.FechaNacimiento.Year == 1)
                    {
                        ModelState.AddModelError("FechaNacimiento", "La Fecha Nacimiento es obligatorio");
                        datosValidos = false;
                    }
                    if (datosValidos)
                    {
                        AutorModel model = new AutorModel();
                        model.IdAutor = autorViewModel.id;
                        model.Nombre = autorViewModel.Nombre;
                        model.FechaNacimiento = autorViewModel.FechaNacimiento;
                        model.Apellido = autorViewModel.Apellido;
                        string resultado = AutorServicio.ModificarAutorApi(model, url);
                        if (resultado != "")
                        {
                            return RedirectToAction("Index", new { mensaje = resultado});
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error al modificar el autor");
                        }
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }
            }

            return View(autorViewModel);
        }

        public ActionResult ListarLibro(int IdAutor, string mensaje)
        {
            AutorViewModel autorViewModel = new AutorViewModel();

            //Conusltar el autor 
            AutorModel autorModel = AutorServicio.ConsultarAutorIDApi(IdAutor, url);

            autorViewModel.Apellido = autorModel.Apellido;
            autorViewModel.Nombre = autorModel.Nombre;
            autorViewModel.FechaNacimiento = autorModel.FechaNacimiento;
            autorViewModel.id = IdAutor;
            autorViewModel.ListaLibros = AutorServicio.ConsultarLibrosAutorAPI(IdAutor, url);

            ViewBag.IdLibro = new SelectList(LibroServicio.ConsultarLibrosApi(url), "IdLibro", "Nombre");
            if(mensaje != null)
            {
                ModelState.AddModelError("", mensaje);
            }
            return View(autorViewModel);
        }

        [HttpPost]
        public ActionResult ListarLibro(string consulta, AutorViewModel autorViewModel)
        {
            ViewBag.IdLibro = new SelectList(LibroServicio.ConsultarLibrosApi(url), "IdLibro", "Nombre");

            if(consulta.Equals("Regresar"))
            {
                return RedirectToAction("Index");
            }
            if (consulta.Equals("Adicionar"))
            {
                if(autorViewModel.IdLibro == 0)
                {
                    ModelState.AddModelError("", "Debe seleccionar algun libro");
                }
                else
                {
                    try
                    {
                        AutorLibroModel request = new AutorLibroModel();
                        request.IdAutor= autorViewModel.id;
                        request.IdLibro= autorViewModel.IdLibro;

                        //consultar si ya el libro esta asignado.
                        AutorLibroModel libroasignado = AutorServicio.ConsultarLibroAsignadoAPI(autorViewModel.IdLibro, url);
                        if (libroasignado.Nombre == null)
                        {
                            string resultado = AutorServicio.AdicionarLibroAutorApi(request, url);
                            ModelState.AddModelError("", resultado);
                        }
                        else{
                            ModelState.AddModelError("", "El libro ya esta asignado a un autor");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",ex.Message);
                    }
                }

            }

            autorViewModel.ListaLibros = AutorServicio.ConsultarLibrosAutorAPI(autorViewModel.id, url);


            return View(autorViewModel);
        }

        public ActionResult EliminarLibroAutor(int IdAutor, int IdLibro)
        {
            AutorViewModel autorViewModel = new AutorViewModel();
            autorViewModel.IdLibro = IdLibro;
            autorViewModel.id = IdAutor;

            //consultar libro
            LibroModel libromodel= LibroServicio.ConsultarLibroIDApi(IdLibro, url);
            autorViewModel.NombreLibro = libromodel.Nombre;
            //consultar autor
             AutorModel autorModel= AutorServicio.ConsultarAutorIDApi(IdAutor, url);
            autorViewModel.Nombre = autorModel.Nombre;

            return View(autorViewModel);
        }

        [HttpPost]
        public ActionResult EliminarLibroAutor(string consulta, AutorViewModel autorViewModel)
        {
            if(consulta.Equals("Eliminar"))
            {
                try
                {
                    string resultado = AutorServicio.EliminarLibroAutorApi(autorViewModel.id, autorViewModel.IdLibro, url);
                    if (resultado.Contains("correctamente"))
                    {
                        return RedirectToAction("ListarLibro", new { IdAutor  = autorViewModel.id, mensaje= resultado});

                    }
                    else
                    {
                        ModelState.AddModelError("", resultado);
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            if(consulta.Equals("Regresar"))
            {
                return RedirectToAction("ListarLibro", new { IdAutor  = autorViewModel.id});
            }
            return View();
        }


    }
}
