using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pajares_EF.Models;

namespace Pajares_EF.BD.Maps
{
    public class CuentaMap : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuenta");
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Transaccion).
              WithOne(o => o.cuentas).
              HasForeignKey(o => o.IdCuenta);

        }
    }
}
