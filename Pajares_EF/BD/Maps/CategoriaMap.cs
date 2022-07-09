using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pajares_EF.Models;
namespace Pajares_EF.BD.Maps

{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Cuenta).
              WithOne(o => o.Categorias).
              HasForeignKey(o => o.IdCategoria);

        }
    }
}
