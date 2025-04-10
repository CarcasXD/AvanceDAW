using AvanceDAW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AvanceDAW.Controllers
{
    public class LoginController : Controller
    {

        private readonly RestauranteDbContext _context;

        public LoginController(RestauranteDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("email");
            var rol = HttpContext.Session.GetString("rol");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(rol))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Email = email;
            ViewBag.Rol = rol;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var usuario = await (from e in _context.Empleados
                                 join r in _context.Roles on e.RolID equals r.RolID
                                 where e.Email == email && e.Contrasena == password
                                 select new
                                 {
                                     Email = e.Email,
                                     Contrasena = e.Contrasena,
                                     Nombre = r.Nombre
                                 }).FirstOrDefaultAsync();

            if (usuario != null)
            {
                Console.WriteLine($"Rol del usuario: {usuario.Nombre}");
                HttpContext.Session.SetString("email", usuario.Email);
                HttpContext.Session.SetString("rol", usuario.Nombre);



                if (usuario.Nombre == "Cocinero")
                {
                    return RedirectToAction("Index", "Cocina");
                }
                else if (usuario.Nombre == "Cajero")
                {
                    return RedirectToAction("Index", "Cajero");
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                    return RedirectToAction("Index", "Home");
                }

            }

            ViewBag.ErrorMessage = "Credenciales incorrectas";
            return View();
        }
    }
}