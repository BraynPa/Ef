using NUnit.Framework;
using Pajares_EF.Models;
using Pajares_EF.Repository;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Pajares_EF.BD;
using Pajares_EF.Test.Helper;

namespace Pajares_EF.Test.TestRepos
{
    [TestFixture]
    public class UsuarioRepositoryTest
    {
        IQueryable<Usuario>? data;
        [SetUp]
        public void setup()
        {
            data = new List<Usuario>
           {
               new() {Id = 100, Username = "User2",  Password ="1234"},
               new() {Id = 101, Username = "Nuevo2",  Password ="1234"},
               new() {Id = 102, Username = "Usuario2",  Password ="1234"}
           }.AsQueryable();
        }
        [Test]
        public void EncontrarUsuarioTest()
        {
            var mockDbSetUsuario = new MockDBSet<Usuario>(data);
            var mockDB = new Mock<EFContext>();
            mockDB.Setup(o => o._usuario).Returns(mockDbSetUsuario.Object);

            var repo = new UsuarioRepository(mockDB.Object, null);
            var rpta = repo.EncontrarUsuario("User2","1234");
            Assert.IsNotNull(rpta);
        }
        [Test]
        public void BuscarUsuarioUserTest()
        {
            var mockDbSetUsuario = new MockDBSet<Usuario>(data);
            var mockDB = new Mock<EFContext>();
            mockDB.Setup(o => o._usuario).Returns(mockDbSetUsuario.Object);

            var repo = new UsuarioRepository(mockDB.Object, null);
            var rpta = repo.BuscarUsuarioUser("User2");
            Assert.IsNotNull(rpta);
        }
        [Test]
        public void VoidAgregarUsuarioTest()
        {
            var mockDbSetUsuario = new MockDBSet<Usuario>(data);
            var mockDB = new Mock<EFContext>();
            mockDB.Setup(o => o._usuario).Returns(mockDbSetUsuario.Object);

            var repo = new UsuarioRepository(mockDB.Object, null);
            Usuario nuevo = new Usuario()
            {
                Username = "User2",
                Password = "1234", 
            };
            repo.AgregarUsuario(nuevo);

            
        }
    }
}
