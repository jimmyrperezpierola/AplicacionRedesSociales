using AplicacionAdministracionRedesSociales.Data;
using AplicacionAdministracionRedesSociales.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TestPrjRedesSociales
{
    public class PublicacionTests
    {
        //private DbContextOptions<RedesSocialesContext> GetInMemoryDatabaseOptions()
        //{
        //    return new DbContextOptionsBuilder<RedesSocialesContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDb")
        //        .Options;
        //}
        private DbContextOptions<RedesSocialesContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<RedesSocialesContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Publicaciones")
                .Options;
        }

        [Fact]
        public void CrearPublicacion()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var publicacion = new Publicacion { Id = 1, Contenido = "Nueva publicación" };
                context.Publicaciones.Add(publicacion);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                Assert.Equal(1, context.Publicaciones.Count());
            }
        }

        [Fact]
        public void ObtenerPublicacionPorId()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                context.Publicaciones.Add(new Publicacion { Id = 1,Contenido = "Test Contentido", UsuarioId= 1 });
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var contenido = context.Publicaciones.Find(1);
                Assert.NotNull(contenido);
                Assert.Equal("Test Contentido", contenido.Contenido);
            }
        }

        [Fact]
        public void ListarPublicaciones()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                context.Publicaciones.Add(new Publicacion { Contenido = "Contenido 1" });
                context.Publicaciones.Add(new Publicacion { Contenido = "Contenido 2" });
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var publicaciones = context.Publicaciones.ToList();
                Assert.Equal(2, publicaciones.Count);
            }
        }

        [Fact]
        public void ModificarPublicacion()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var publicacion = new Publicacion { Contenido = "Contenido original" };
                context.Publicaciones.Add(publicacion);
                context.SaveChanges();

                publicacion.Contenido = "Contenido editado";
                context.Publicaciones.Update(publicacion);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var publicacion = context.Publicaciones.FirstOrDefault();
                Assert.NotNull(publicacion);
                Assert.Equal("Contenido editado", publicacion.Contenido);
            }
        }

        [Fact]
        public void EliminarPublicacion()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var publicacion = new Publicacion { Contenido = "Contenido a eliminar" };
                context.Publicaciones.Add(publicacion);
                context.SaveChanges();

                context.Publicaciones.Remove(publicacion);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var publicacion = context.Publicaciones.FirstOrDefault();
                Assert.Null(publicacion);
            }
        }
    }
}
