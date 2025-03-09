using AplicacionAdministracionRedesSociales.Data;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAdministracionRedesSociales.Models
{
    public class Publicacion
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public static void Registrar(RedesSocialesContext context, Publicacion publicacion)
        {
            context.Publicaciones.Add(publicacion);
            context.SaveChanges();
        }

        public static List<Publicacion> Listar(RedesSocialesContext context)
        {
            return context.Publicaciones.Include(p => p.Usuario).Include(p => p.Contenido).ToList();
        }

        public static Publicacion Buscar(RedesSocialesContext context, int id)
        {
            return context.Publicaciones.Include(p => p.Usuario).Include(p => p.Contenido).FirstOrDefault(p => p.Id == id);
        }

        public static void Modificar(RedesSocialesContext context, Publicacion publicacion)
        {
            context.Publicaciones.Update(publicacion);
            context.SaveChanges();
        }

        public static void Eliminar(RedesSocialesContext context, int id)
        {
            var publicacion = context.Publicaciones.Find(id);
            if (publicacion != null)
            {
                context.Publicaciones.Remove(publicacion);
                context.SaveChanges();
            }
        }
    }
}
