namespace Pajares_EF.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        public int IdCategoria { get; set; }
        public decimal Saldo { get; set; }
        public int IdMoneda { get; set; }
        public Monedas? Monedass { get; set; }
        public Categoria? Categorias { get; set; }
        public List<Transaccion>? Transaccion { get; set; }
    }
}
