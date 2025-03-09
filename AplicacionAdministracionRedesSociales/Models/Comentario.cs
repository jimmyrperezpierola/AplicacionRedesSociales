using AplicacionAdministracionRedesSociales.Data;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAdministracionRedesSociales.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; }
        public int PublicacionId { get; set; }
        public Publicacion Publicacion { get; set; }

        public static void Registrar(RedesSocialesContext context, Comentario comentario)
        {
            context.Comentarios.Add(comentario);
            context.SaveChanges();
        }

        public static List<Comentario> Listar(RedesSocialesContext context)
        {
            return context.Comentarios.Include(c => c.Publicacion).ToList();
        }

        public static Comentario Buscar(RedesSocialesContext context, int id)
        {
            return context.Comentarios.Include(c => c.Publicacion).FirstOrDefault(c => c.Id == id);
        }

        public static void Modificar(RedesSocialesContext context, Comentario comentario)
        {
            context.Comentarios.Update(comentario);
            context.SaveChanges();
        }

        public static void Eliminar(RedesSocialesContext context, int id)
        {
            var comentario = context.Comentarios.Find(id);
            if (comentario != null)
            {
                context.Comentarios.Remove(comentario);
                context.SaveChanges();
            }
        }
    }
}
