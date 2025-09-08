using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class ObraSocialConfiguration : IEntityTypeConfiguration<ObraSocial>
    {
        public void Configure(EntityTypeBuilder<ObraSocial> builder)
        {
            builder.ToTable("obra_social", "agendasalud");

            builder.HasKey(o => o.Id);

            builder.HasMany(c => c.PacienteAfiliados)
                   .WithOne(o => o.ObraSocial)
                   .HasForeignKey(o => o.ObraSocialId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}