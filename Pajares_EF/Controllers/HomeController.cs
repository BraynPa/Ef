using Microsoft.AspNetCore.Mvc;
using Pajares_EF.Models;
using Pajares_EF.Repository;
using Pajares_EF.Service;
using System.Diagnostics;

namespace Pajares_EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioRepository _context;
        private readonly ICookieAuthService _cookieAuthService;
        private readonly ICuentaRepository _repository;

        [Obsolete]
        public HomeController(IUsuarioRepository _context, ICookieAuthService _cookieAuthService, ICuentaRepository _repository)
        {
            this._context = _context;
            this._cookieAuthService = _cookieAuthService;
            this._repository = _repository;
            _cookieAuthService.SetHttpContext(HttpContext);
        }

        public IActionResult Index()
        {
            _cookieAuthService.SetHttpContext(HttpContext);
            ViewBag.Nombre = _cookieAuthService.LoggedUser().Username;
            ViewBag.Cuentas = _repository.ListaCuenta();
            ViewBag.Transacciones = _repository.ListaTransacciones();
            return View();
        }
        [HttpGet]
        public IActionResult CreateCuenta()
        {
            ViewBag.Categoria = _repository.Categorias();
            ViewBag.MMoneda = _repository.Monedas();
            return View(new Cuenta());

        }
        [HttpPost]
        public IActionResult CreateCuenta(Cuenta cuenta)
        {
            if (cuenta.Nombre == null)
            {
                ModelState.AddModelError("Nombre", "Nombre vacio");
            }
            if (cuenta.IdCategoria <= 0 || cuenta.IdCategoria >= 3)
            {
                ModelState.AddModelError("IdCategoria", "IdCategoria No valido");
            }
            if (cuenta.IdMoneda <= 0 || cuenta.IdMoneda >= 3)
            {
                ModelState.AddModelError("IdMoneda", "IdMoneda No valido");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categoria = _repository.Categorias();
                ViewBag.MMoneda = _repository.Monedas();
                return View("CreateCuenta", cuenta);
            }
            _repository.RegistrarCuenta(cuenta);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult CreateTransaccionIngreso()
        {
            ViewBag.Cuentas = _repository.ListaCuenta();
            return View(new Transaccion());

        }
        [HttpPost]
        public IActionResult CreateTransaccionIngreso(Transaccion transaccion)
        {
            var cuenta = _repository.EncontrarCuenta(transaccion.IdCuenta);
            DateTime fechaActual = DateTime.Now;
            transaccion.Fecha = fechaActual;
            if (transaccion.Descripcion == null)
            {
                ModelState.AddModelError("Descripcion", "Descripcion vacio");
            }
            if(transaccion.IdCuenta <= 0)
            {
                ModelState.AddModelError("IdCuenta", "IdCuentas No valido");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Cuentas = _repository.ListaCuenta();
                return View("CreateTransaccionIngreso", transaccion);
            }
            _repository.RegistrarTransaccionIngreso(transaccion);
            if (cuenta.IdCategoria == 1)
            {
                _repository.UpdateSaldoIngreso(transaccion.IdCuenta, transaccion.Monto);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
        public IActionResult CreateTransaccionGasto()
        {
            ViewBag.Cuentas = _repository.ListaCuenta();
            return View(new Transaccion());

        }
        [HttpPost]
        public IActionResult CreateTransaccionGasto(Transaccion transaccion)
        {
            var cuenta = _repository.EncontrarCuenta(transaccion.IdCuenta);
            DateTime fechaActual = DateTime.Now;
            transaccion.Fecha = fechaActual;
            if (transaccion.Descripcion == null)
            {
                ModelState.AddModelError("Descripcion", "Descripcion vacio");
            }
            if (transaccion.IdCuenta <= 0)
            {
                ModelState.AddModelError("IdCuenta", "IdCuentas No valido");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Cuentas = _repository.ListaCuenta();
                return View("CreateTransaccionGasto", transaccion);
            }
            _repository.RegistrarTransaccionGasto(transaccion);
            if (cuenta.IdCategoria == 1)
            {
                if (_repository.ValidarTransaccionPropio(transaccion))
                {
                    
                    _repository.UpdateSaldoGasto(transaccion.IdCuenta, transaccion.Monto);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("TransaccionErronea");
                }
                
            }
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult TransaccionErronea()
        {
            return View();

        }
        [HttpGet]
        public IActionResult Total(decimal Saldo)
        {
            ViewBag.Monto = _repository.CambioASoles(Saldo);
            return View();

        }
        [HttpGet]
        public IActionResult Transaccion()
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}