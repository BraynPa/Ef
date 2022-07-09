namespace Pajares_EF.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public String? Tipo { get; set; }
        public List<Cuenta>? Cuenta { get; set; }
    }
}
