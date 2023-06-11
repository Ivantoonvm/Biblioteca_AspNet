using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Proyecto_biblioteca.Models;
using Proyecto_biblioteca.Logica;

namespace Proyecto_biblioteca.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string correo, string clave)
        {
            Persona ousuario = PersonaLogica.Instancia.Listar().Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefault();
            
            
            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }
            
            // Crear una cookie para almacenar el usuario
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1) // Configurar la expiración de la cookie
            };
            Response.Cookies.Append("Usuario", ousuario.oTipoPersona.IdTipoPersona.ToString(), options);
            Response.Cookies.Append("Nombre", ousuario.Nombre,options);
            Response.Cookies.Append("Apellido", ousuario.Apellido,options);

            return RedirectToAction("Index", "Admin"); // Redireccionar a la página de inicio después del inicio de sesión exitoso 
        }
    }
}