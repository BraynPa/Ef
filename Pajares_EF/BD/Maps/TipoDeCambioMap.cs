using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pajares_EF.Models;

namespace Pajares_EF.BD.Maps
{
    public class TipoDeCambioMap : IEntityTypeConfiguration<TipoDeCambio>
    {
        public void Configure(EntityTypeBuilder<TipoDeCambio> builder)
        {
            builder.ToTable("TipoDeCambio");
            builder.HasKey(o => o.Id);


        }
    }
}
