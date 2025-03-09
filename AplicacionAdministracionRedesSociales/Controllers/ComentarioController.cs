using AplicacionAdministracionRedesSociales.Data;
using AplicacionAdministracionRedesSociales.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionAdministracionRedesSociales.Controllers
{
    public class ComentarioController : Controller
    {
        private readonly RedesSocialesContext _context;

        public ComentarioController(RedesSocialesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var comentarios = Comentario.Listar(_context);
            return View(comentarios);
        }

        public IActionResult GetComentario(int id)
        {
            var comentario = Comentario.Buscar(_context, id);
            if (comentario == null)
            {
                return NotFound();
            }
            return View(comentario);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                Comentario.Registrar(_context, comentario);
                return RedirectToAction(nameof(Index));
            }
            return View(comentario);
        }

        public IActionResult Editar(int id)
        {
            var comentario = Comentario.Buscar(_context, id);
            if (comentario == null)
            {
                return NotFound();
            }
            return View(comentario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                Comentario.Modificar(_context, comentario);
                return RedirectToAction(nameof(Index));
            }
            return View(comentario);
        }

        public IActionResult Eliminar(int id)
        {
            var comentario = Comentario.Buscar(_context, id);
            if (comentario == null)
            {
                return NotFound();
            }
            return View(comentario);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int id)
        {
            Comentario.Eliminar(_context, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
