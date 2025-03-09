using AplicacionAdministracionRedesSociales.Data;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAdministracionRedesSociales.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Email { get; set; }
        public List<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();

        public static void Registrar(RedesSocialesContext context, Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public static List<Usuario> Listar(RedesSocialesContext context)
        {
            return context.Usuarios.Include(u => u.Publicaciones).ToList();
        }

        public static Usuario Buscar(RedesSocialesContext context, int id)
        {
            return context.Usuarios.Include(u => u.Publicaciones).FirstOrDefault(u => u.Id == id);
        }

        public static void Modificar(RedesSocialesContext context, Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            context.SaveChanges();
        }

        public static void Eliminar(RedesSocialesContext context, int id)
        {
            var usuario = context.Usuarios.Find(id);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }
        }
    }
}
