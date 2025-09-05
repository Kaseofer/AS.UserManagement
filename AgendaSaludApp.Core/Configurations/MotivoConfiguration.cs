using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class MotivoConfiguration : IEntityTypeConfiguration<Motivo>
    {
        public void Configure(EntityTypeBuilder<Motivo> builder)
        {
            builder.ToTable("motivo", "agendasalud");

            builder.HasKey(m => m.Id);


        }
    }
}