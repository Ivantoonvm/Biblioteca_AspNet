using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Proyecto_biblioteca.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Verificar si la cookie de sesión existe y contiene el identificador correcto
            if (Request.Cookies.TryGetValue("Usuario", out string sessionId))
            {
                // El usuario está autenticado, realizar las operaciones necesarias

                return View();
            }

            // Si la cookie de sesión no existe o el identificador es incorrecto, redirigir al inicio de sesión
            return RedirectToAction("Index", "Login");
        }

        public IActionResult CerrarSesion()
        {
            // Eliminar la cookie de sesión
            Response.Cookies.Delete("Usuario");
            Response.Cookies.Delete("Nombre");
            Response.Cookies.Delete("Apellido");
            return RedirectToAction("Index", "Login");
        }
    }
}