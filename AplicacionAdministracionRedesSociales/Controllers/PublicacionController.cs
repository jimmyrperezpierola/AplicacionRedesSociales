using AplicacionAdministracionRedesSociales.Data;
using AplicacionAdministracionRedesSociales.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionAdministracionRedesSociales.Controllers
{
    public class PublicacionController : Controller
    {
        private readonly RedesSocialesContext _context;

        public PublicacionController(RedesSocialesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var publicaciones = Publicacion.Listar(_context);
            return View(publicaciones);
        }

        public IActionResult GetPublicacion(int id)
        {
            var publicacion = Publicacion.Buscar(_context, id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return View(publicacion);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                Publicacion.Registrar(_context, publicacion);
                return RedirectToAction(nameof(Index));
            }
            return View(publicacion);
        }

        public IActionResult Editar(int id)
        {
            var publicacion = Publicacion.Buscar(_context, id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return View(publicacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                Publicacion.Modificar(_context, publicacion);
                return RedirectToAction(nameof(Index));
            }
            return View(publicacion);
        }

        public IActionResult Eliminar(int id)
        {
            var publicacion = Publicacion.Buscar(_context, id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return View(publicacion);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int id)
        {
            Publicacion.Eliminar(_context, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
