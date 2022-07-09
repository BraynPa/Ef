namespace Pajares_EF.Models
{
    public class Monedas
    {
        public int Id { get; set; }
        public String? Moneda { get; set; }
        public List<Cuenta>? Cuenta { get; set; }
    }
}
