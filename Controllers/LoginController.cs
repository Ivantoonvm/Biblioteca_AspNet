using Proyecto_biblioteca.Logica;
using Proyecto_biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Proyecto_biblioteca.Controllers;

    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {

            Persona ousuario = PersonaLogica.Instancia.Listar().Where(u => u.Correo == correo && u.Clave == clave && u.oTipoPersona.IdTipoPersona != 3).FirstOrDefault();

            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            // Session["Usuario"] = ousuario;

            return RedirectToAction("Index", "Admin");
        }
    }
