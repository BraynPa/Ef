using Microsoft.EntityFrameworkCore;
using Pajares_EF.BD.Maps;
using Pajares_EF.Models;

namespace Pajares_EF.BD
{
    public interface IEFContext
    {
        DbSet<Usuario> _usuario { get; set; }
        DbSet<Categoria> _categoria { get; set; }
        DbSet<Cuenta> _cuenta { get; set; }
        DbSet<Monedas> _monedas { get; set; }
        DbSet<TipoDeCambio> _tipoDeCambio { get; set; }
        DbSet<Transaccion> _transaccion { get; set; }
        int SaveChanges();

    }
    public class EFContext : DbContext, IEFContext
    {

        public virtual DbSet<Usuario> _usuario { get; set; }
        public virtual DbSet<Categoria> _categoria { get; set; }
        public virtual DbSet<Cuenta> _cuenta { get; set; }
        public virtual DbSet<Monedas> _monedas { get; set; }
        public virtual DbSet<TipoDeCambio> _tipoDeCambio { get; set; }
        public virtual DbSet<Transaccion> _transaccion { get; set; }

        public EFContext()
        {

        }
        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CuentaMap());
            modelBuilder.ApplyConfiguration(new MonedasMap());
            modelBuilder.ApplyConfiguration(new TipoDeCambioMap());
            modelBuilder.ApplyConfiguration(new TransaccionMap());

        }
    }
}
