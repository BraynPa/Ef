using Microsoft.EntityFrameworkCore;
using Pajares_EF.BD;
using Pajares_EF.Models;

namespace Pajares_EF.Repository
{
    public interface ICuentaRepository
    {
        public Cuenta EncontrarCuenta(int id);
        public decimal CambioASoles(decimal monto);
        List<Cuenta> ListaCuenta();
        List<Categoria> Categorias();
        List<Monedas> Monedas();
        List<Transaccion> ListaTransaccionPorId(int Id);
        List<Transaccion> ListaTransacciones();
        public void RegistrarCuenta(Cuenta cuenta);
        public void RegistrarTransaccionGasto(Transaccion transaccion);
        public void RegistrarTransaccionIngreso(Transaccion transaccion);
        public bool ValidarTransaccionPropio(Transaccion transaccion);
        public void UpdateSaldoIngreso(int id,decimal monto);
        public void UpdateSaldoGasto(int id, decimal monto);

    }
    public class CuentaRepository : ICuentaRepository
    {
        private EFContext _context;
        public CuentaRepository(EFContext context)
        {
            _context = context;
        }
        public Cuenta EncontrarCuenta(int id)
        {
            var cuenta = _context._cuenta.FirstOrDefault(o => o.Id == id);
            return cuenta;
        }
        public decimal CambioASoles(decimal monto)
        {
            var cambio = _context._tipoDeCambio.FirstOrDefault(o => o.Id == 1);
            return monto*cambio.Valor;
        }
        public List<Cuenta> ListaCuenta()
        {
            return _context._cuenta.Include(s => s.Categorias).Include(l => l.Monedass).ToList();
        }
        public List<Categoria> Categorias()
        {
            return _context._categoria.ToList();
        }
        public List<Monedas> Monedas()
        {
            return _context._monedas.ToList();
        }
        public List<Transaccion> ListaTransaccionPorId(int Id)
        {
            return _context._transaccion.Where(s => s.IdCuenta == Id).OrderByDescending(n => n.Fecha).ToList();
        }
        public List<Transaccion> ListaTransacciones()
        {
            return _context._transaccion.OrderByDescending(n => n.Fecha).ToList();

        }
        public void RegistrarCuenta(Cuenta cuenta)
        {
            _context._cuenta.Add(cuenta);
            _context.SaveChanges();

        }
        public void UpdateSaldoGasto(int id, decimal monto)
        {
            var cuenta = _context._cuenta.FirstOrDefault(o => o.Id == id);
            decimal monto1 = cuenta.Saldo;
            monto1 -= monto;
            cuenta.Saldo = monto1;
            _context._cuenta.Update(cuenta);
            _context.SaveChanges();

        }
        public void UpdateSaldoIngreso(int id, decimal monto)
        {
            var cuenta = _context._cuenta.FirstOrDefault(o => o.Id == id);
            decimal monto1 = cuenta.Saldo;
            monto1 += monto;
            cuenta.Saldo = monto1;
            _context._cuenta.Update(cuenta);
            _context.SaveChanges();

        }
        public void RegistrarTransaccionGasto(Transaccion transaccion)
        {
            transaccion.Monto *= -1;
            _context._transaccion.Add(transaccion);
            _context.SaveChanges();

        }
        public void RegistrarTransaccionIngreso(Transaccion transaccion)
        {
            _context._transaccion.Add(transaccion);
            _context.SaveChanges();

        }
        public bool ValidarTransaccionPropio(Transaccion transaccion)
        {
            
                transaccion.Monto *= -1;
                var cuenta = _context._cuenta.FirstOrDefault(o => o.Id == transaccion.IdCuenta);
                cuenta.Saldo += transaccion.Monto;
                if(cuenta.Saldo >= 0)
                {
                    return true;
                }
                else { return false; }
        }

    }
}
