using Microsoft.AspNetCore.Mvc;


namespace Proyecto_biblioteca.Controllers;

    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Lectores()
        {
            return View();
        }

        
    }
