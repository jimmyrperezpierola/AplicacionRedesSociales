using AplicacionAdministracionRedesSociales.Data;
using AplicacionAdministracionRedesSociales.Models;
using Microsoft.EntityFrameworkCore;

namespace TestPrjRedesSociales
{
    public class UsuarioTests
    {
        private DbContextOptions<RedesSocialesContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<RedesSocialesContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        //[Fact]
        //public void Test1()
        //{

        //}

        [Fact]
        public void RegistrarUsuario()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var usuario = new Usuario { Id = 1, Nombre = "Jimmy Perez" };
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                Assert.Equal(1, context.Usuarios.Count());
            }
        }

        [Fact]
        public void ObtenerUsuarioPorId()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                context.Usuarios.Add(new Usuario { Id = 1, Nombre = "Jimmy Perez" });
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var usuario = context.Usuarios.Find(1);
                Assert.NotNull(usuario);
                Assert.Equal("Jimmy Perez", usuario.Nombre);
            }
        }

        [Fact]
        public void ListarUsuarios()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                context.Usuarios.Add(new Usuario { Nombre = "Rita Urgarte" });
                context.Usuarios.Add(new Usuario { Nombre = "Sofia Torrez" });
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var usuarios = context.Usuarios.ToList();
                Assert.Equal(2, usuarios.Count);
            }
        }

        [Fact]
        public void ModificarUsuario()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var usuario = new Usuario { Nombre = "Jimmy Perez", Email = "pila900@gmail.com" };
                context.Usuarios.Add(usuario);
                context.SaveChanges();

                usuario.Nombre = "Jimmy Perez Pierola";
                context.Usuarios.Update(usuario);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.Email == "pila900@gmail.com");
                Assert.NotNull(usuario);
                Assert.Equal("Jimmy Perez Pierola", usuario.Nombre);
            }
        }

        [Fact]
        public void EliminarUsuario()
        {
            var options = GetDbContextOptions();
            using (var context = new RedesSocialesContext(options))
            {
                var usuario = new Usuario { Nombre = "testuser", Email = "testuser@gmail.com" };
                context.Usuarios.Add(usuario);
                context.SaveChanges();

                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }

            using (var context = new RedesSocialesContext(options))
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.Email == "testuser@gmail.com");
                Assert.Null(usuario);
            }
        }
    }
}