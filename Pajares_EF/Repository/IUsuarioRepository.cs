using Pajares_EF.BD;
using Pajares_EF.Models;
using Pajares_EF.Service;
using System.Security.Claims;

namespace Pajares_EF.Repository
{
    public interface IUsuarioRepository
    {
        public Usuario ObtenerUsuarioLogin(Claim claim);
        public Usuario EncontrarUsuario(String user, String password);
        public Dictionary<int, String> IndicesPorId();
        public bool BuscarUsuarioUser(String user);
        public void AgregarUsuario(Usuario usuario);
    }


    public class UsuarioRepository : IUsuarioRepository
    {
        private IEFContext _context;
        private readonly ICookieAuthService _cookieAuthService;

        public UsuarioRepository(EFContext context, ICookieAuthService cookieAuthService)
        {
            _context = context;
            _cookieAuthService = cookieAuthService;

        }
        public Usuario EncontrarUsuario(string user, string password)
        {
            var Usuario = _context._usuario.FirstOrDefault(o => o.Username == user && o.Password == password);
            return Usuario;
        }


        public Dictionary<int, string> IndicesPorId()
        {
            Dictionary<int, string> indices = new Dictionary<int, string>();
            var usuarios = _context._usuario.ToList();

            foreach (var item in usuarios)
            {
                indices.Add(item.Id, item.Username);
            }

            return indices;
        }

        public Usuario ObtenerUsuarioLogin(Claim claim)
        {
            var user = _context._usuario.FirstOrDefault(o => o.Username == claim.Value);
            return user;
        }
        public bool BuscarUsuarioUser(String user)
        {
            var Usuario = _context._usuario.FirstOrDefault(o => o.Username == user);
            if (Usuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void AgregarUsuario(Usuario usuario)
        {
            _context._usuario.Add(usuario);
            _context.SaveChanges();
        }
    }
}
