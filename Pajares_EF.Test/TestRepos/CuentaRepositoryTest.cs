using NUnit.Framework;
using Pajares_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajares_EF.Test.TestRepos
{
    [TestFixture]
    public class CuentaRepositoryTest
    {
        IQueryable<Cuenta>? data;
        [SetUp]
        public void setup()
        {
            data = new List<Cuenta>
           {
               new() {Id = 100, Nombre = "User2",  IdCategoria = 1, Saldo = 1000, IdMoneda= 1},
               new() {Id = 101, Nombre = "User1",  IdCategoria = 2, Saldo = 2000, IdMoneda= 2}
           }.AsQueryable();
        }
        
    }
}
