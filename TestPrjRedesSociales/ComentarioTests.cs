using AplicacionAdministracionRedesSociales.Data;
using AplicacionAdministracionRedesSociales.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TestPrjRedesSociales
{
    public class ComentarioTests
    {
        private DbContextOptions<RedesSocialesContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<RedesSocialesContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Comentarios")
                .Options;
        }

        [Fact]
        public void RegistrarComentario()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var comentario = new Comentario { Id = 1, Contenido = "Contenido de prueba", FechaComentario = new DateTime(), PublicacionId = 1 };
                context.Comentarios.Add(comentario);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                Assert.Equal(1, context.Comentarios.Count());
            }
        }

        [Fact]
        public void ObtenerComentarioPorId()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                context.Comentarios.Add(new Comentario { Id = 1, Contenido= "Test Comentario" });
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var comentario = context.Comentarios.Find(1);
                Assert.NotNull(comentario);
                Assert.Equal("Test Comentario", comentario.Contenido);
            }
        }

        [Fact]
        public void ListarComentarios()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                context.Comentarios.Add(new Comentario { Contenido = "Primer comentario" });
                context.Comentarios.Add(new Comentario { Contenido = "Segundo comentario" });
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var comentarios = context.Comentarios.ToList();
                Assert.Equal(2, comentarios.Count);
            }
        }

        [Fact]
        public void ModificarComentario()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var comentario = new Comentario { Contenido = "Comentario original" };
                context.Comentarios.Add(comentario);
                context.SaveChanges();

                comentario.Contenido = "Comentario editado";
                context.Comentarios.Update(comentario);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var comentario = context.Comentarios.FirstOrDefault();
                Assert.NotNull(comentario);
                Assert.Equal("Comentario editado", comentario.Contenido);
            }
        }

        [Fact]
        public void EliminarComentario()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var comentario = new Comentario { Contenido = "Test Comentario a eliminar" };
                context.Comentarios.Add(comentario);
                context.SaveChanges();

                context.Comentarios.Remove(comentario);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var comentario = context.Comentarios.FirstOrDefault();
                Assert.Null(comentario);
            }
        }
    }
}
