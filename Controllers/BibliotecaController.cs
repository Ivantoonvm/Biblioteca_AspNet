using Proyecto_biblioteca.Logica;
using Proyecto_biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;



namespace Proyecto_biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        private readonly IConfiguration _configuration;

        public BibliotecaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: Biblioteca
        public IActionResult Libros()
        {
            return View();
        }

        public IActionResult Autores()
        {
            return View();
        }

        public IActionResult Editorial()
        {
            return View();
        }

        public IActionResult Categoria()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = CategoriaLogica.Instancia.Listar();
            return Json(new { data = oLista });
        }
        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdCategoria == 0) ? CategoriaLogica.Instancia.Registrar(objeto) : CategoriaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta });
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int IdCategoria)
        {
            bool respuesta = false;
            respuesta = CategoriaLogica.Instancia.Eliminar(IdCategoria);
            return Json(new { resultado = respuesta });
        }



        [HttpGet]
        public IActionResult ListarEditorial()
        {
            List<Editorial> oLista = new List<Editorial>();
            oLista = EditorialLogica.Instancia.Listar();
            return Json(new { data = oLista });
        }
        [HttpPost]
        public JsonResult GuardarEditorial(Editorial objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdEditorial == 0) ? EditorialLogica.Instancia.Registrar(objeto) : EditorialLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta });
        }
        [HttpPost]
        public JsonResult EliminarEditorial(int IdEditorial)
        {
            bool respuesta = false;
            respuesta = EditorialLogica.Instancia.Eliminar(IdEditorial);
            return Json(new { resultado = respuesta });
        }



        [HttpGet]
        public JsonResult ListarAutor()
        {
            List<Autor> oLista = new List<Autor>();
            oLista = AutorLogica.Instancia.Listar();
            return Json(new { data = oLista });
        }
        [HttpPost]
        public JsonResult GuardarAutor(Autor objeto)
        {
            System.Console.WriteLine(objeto.IdAutor);
            bool respuesta = false;
            respuesta = (objeto.IdAutor == 0) ? AutorLogica.Instancia.Registrar(objeto) : AutorLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta });
        }
        [HttpPost]
        public JsonResult EliminarAutor(int ididAutor)
        {
            bool respuesta = false;
            respuesta = AutorLogica.Instancia.Eliminar(ididAutor);
            return Json(new { resultado = respuesta });
        }



        [HttpGet]
        public JsonResult ListarLibro()
        {
            List<Libro> oLista = new List<Libro>();

            oLista = LibroLogica.Instancia.Listar();
           
            return Json(new { data = oLista });
        }
        [HttpPost]
public JsonResult GuardarLibro(string objeto, IFormFile imagenArchivo)
{
    Response oresponse = new Response() { resultado = true, mensaje = "" };
    try
    {
        Libro oLibro = JsonConvert.DeserializeObject<Libro>(objeto);

        string GuardarEnRuta = "images/";
        oLibro.RutaPortada = GuardarEnRuta;
        oLibro.NombrePortada = "";
        
        
        if (!Directory.Exists(GuardarEnRuta)){
            Directory.CreateDirectory(GuardarEnRuta); 
        }

        if (oLibro.IdLibro == 0)
        {
            int id = LibroLogica.Instancia.Registrar(oLibro);
            oLibro.IdLibro = id;
            oresponse.resultado = oLibro.IdLibro == 0 ? false : true;
        }
        else
        {
            oresponse.resultado = LibroLogica.Instancia.Modificar(oLibro);
        }

        if (imagenArchivo != null && oLibro.IdLibro != 0)
        {
            string extension = Path.GetExtension(imagenArchivo.FileName);
            GuardarEnRuta = Path.Combine(GuardarEnRuta, oLibro.IdLibro.ToString() + extension);
            oLibro.NombrePortada = oLibro.IdLibro.ToString() + extension;

            using (var stream = new FileStream(GuardarEnRuta, FileMode.Create))
            {
                imagenArchivo.CopyTo(stream);
            }

            oresponse.resultado = LibroLogica.Instancia.ActualizarRutaImagen(oLibro);
        }


    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        oresponse.resultado = false;
        oresponse.mensaje = e.Message;
    }
    return Json(oresponse);
}


        [HttpPost]
        public JsonResult EliminarLibro(int IdLibro)
        {
            bool respuesta = false;
            respuesta = LibroLogica.Instancia.Eliminar(IdLibro);
            return Json(new { resultado = respuesta });
        }


        [HttpGet]
        public JsonResult ListarTipoPersona()
        {
            List<TipoPersona> oLista = new List<TipoPersona>();
            oLista = TipoPersonaLogica.Instancia.Listar();
            return Json(new { data = oLista });
        }


        [HttpGet]
        public JsonResult ListarPersona()
        {
            List<Persona> oLista = new List<Persona>();

            oLista = PersonaLogica.Instancia.Listar();

            return Json(new { data = oLista });
        }
        [HttpPost]
        public JsonResult GuardarPersona(Persona objeto)
        {
            bool respuesta = false;
            objeto.Clave = objeto.Clave == null ? GenerarClaveAleatoria() : objeto.Clave;
            respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta });
        }
        [HttpPost]
        public JsonResult EliminarPersona(int IdPersona)
        {
            bool respuesta = false;
            respuesta = PersonaLogica.Instancia.Eliminar(IdPersona);
            return Json(new { resultado = respuesta });
        }

        public string GenerarClaveAleatoria()
    {
           return $"UPIC{Guid.NewGuid().ToString().Substring(0, 8)}";
    }


    }
    public class Response
    {

        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }
    
}