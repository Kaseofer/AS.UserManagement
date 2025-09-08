using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("paciente", "agendasalud");

            builder.HasKey(p => p.Id);

            // Configurar la relación uno a uno entre Paciente y Credencial

            builder.HasOne(p => p.ObraSocial)
                .WithMany(c => c.PacienteAfiliados)
                .HasForeignKey(c => c.ObraSocialId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}