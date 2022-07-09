using Microsoft.AspNetCore.Authentication;
using Pajares_EF.BD;
using Pajares_EF.Models;
using System.Security.Claims;

namespace Pajares_EF.Service
{
    public interface ICookieAuthService
    {
        void SetHttpContext(HttpContext httpContext);
        public void Login(ClaimsPrincipal claim);
        public Claim ObtenerClaim();
        Usuario LoggedUser();
    }


    public class CookieAuthService : ICookieAuthService
    {
        private HttpContext httpContext;
        private IEFContext context;

        public CookieAuthService(EFContext context){

            this.context = context;
        }
        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public void Login(ClaimsPrincipal claim)
        {
            httpContext.SignInAsync(claim);

        }

        public Claim ObtenerClaim()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            return claim;
        }

        public Usuario LoggedUser()
        {

            var claim = httpContext.User.Claims.FirstOrDefault();

            var user = context._usuario.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
