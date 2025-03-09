using AplicacionAdministracionRedesSociales.Data;
using AplicacionAdministracionRedesSociales.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionAdministracionRedesSociales.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly RedesSocialesContext _context;

        public UsuarioController(RedesSocialesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = Usuario.Listar(_context);
            return View(usuarios);
        }

        public IActionResult GetUsuario(int id)
        {
            var usuario = Usuario.Buscar(_context, id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario.Registrar(_context, usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public IActionResult Editar(int id)
        {
            var usuario = Usuario.Buscar(_context, id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario.Modificar(_context, usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public IActionResult Eliminar(int id)
        {
            var usuario = Usuario.Buscar(_context, id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            Usuario.Eliminar(_context, id);
            return RedirectToAction("Index");
        }
    }
}
