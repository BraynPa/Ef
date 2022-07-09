using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pajares_EF.Models;
using Pajares_EF.Repository;
using Pajares_EF.Service;
using System.Security.Claims;

namespace Pajares_EF.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioRepository _usuario;
        private readonly ICookieAuthService _cookieAuthService;
        private readonly ILogger<AuthController> _logger;

        
        public AuthController(IUsuarioRepository _usuario, ICookieAuthService _cookieAuthService, ILogger<AuthController> logger)
        {

            this._usuario = _usuario;
            this._cookieAuthService = _cookieAuthService;
            _cookieAuthService.SetHttpContext(HttpContext);
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            _cookieAuthService.SetHttpContext(HttpContext);
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            _cookieAuthService.SetHttpContext(HttpContext);
            var usuario = _usuario.EncontrarUsuario(username, password);
            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                _cookieAuthService.SetHttpContext(HttpContext);
                _cookieAuthService.Login(claimsPrincipal);


                return RedirectToAction("Index", "Home");
            }

            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        public IActionResult Registrar(Usuario usuario)
        {
            _cookieAuthService.SetHttpContext(HttpContext);
            return View(new Usuario());
        }
        [HttpPost]
        public IActionResult Registrar(Usuario usuario, string passwordConf)
        {
            if (_usuario.BuscarUsuarioUser(usuario.Username) == true)
            {
                ModelState.AddModelError("Username", "Usuario existente");
            }
            if (usuario.Username == null)
            {
                ModelState.AddModelError("UsernameVacio", "Ingresar Usuario");
            }
           
            if (usuario.Password == null)
            {
                ModelState.AddModelError("PasswordVacio", "Ingresar Password");
            }
            if (passwordConf == null)
            {
                ModelState.AddModelError("passwordConfVacio", "Ingresar password Confirmacion");
            }
            if (usuario.Password != passwordConf)
            {
                ModelState.AddModelError("contraseñas", "Contraseñas no coinciden");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Registrar", "Auth", usuario);
            }

            _cookieAuthService.SetHttpContext(HttpContext);
            _usuario.AgregarUsuario(usuario);
            return RedirectToAction("Login", "Auth");
        }



    }
}

