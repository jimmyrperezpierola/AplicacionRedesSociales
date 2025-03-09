using AplicacionAdministracionRedesSociales.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAdministracionRedesSociales.Data
{
    public class RedesSocialesContext: DbContext
    {
        public RedesSocialesContext(DbContextOptions<RedesSocialesContext> contextOptions) : base(contextOptions) { 
        
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=redessocialesdb;user=dev;password=Mstr@93045;",
                    new MySqlServerVersion(new Version(8, 4, 3)));
            }
        }
    }
}
