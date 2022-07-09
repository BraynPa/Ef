using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pajares_EF.Models;

namespace Pajares_EF.BD.Maps
{
    public class MonedasMap : IEntityTypeConfiguration<Monedas>
    {
        public void Configure(EntityTypeBuilder<Monedas> builder)
        {
            builder.ToTable("Monedas");
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Cuenta).
              WithOne(o => o.Monedass).
              HasForeignKey(o => o.IdMoneda);

        }
    }
}
