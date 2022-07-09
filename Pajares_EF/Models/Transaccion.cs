namespace Pajares_EF.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int IdCuenta { get; set; }

        public DateTime Fecha { get; set; }
        public String? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public Cuenta? cuentas { get; set; }
    }
}
