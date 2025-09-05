using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class CredencialConfiguration : IEntityTypeConfiguration<Credencial>
    {
        public void Configure(EntityTypeBuilder<Credencial> builder)
        {
            builder.ToTable("credencial", "agendasalud");

            builder.HasKey(c => c.Id);

            // Relación uno a uno con Paciente
            builder.HasOne(c => c.Paciente)
                   .WithOne(p => p.Credencial) // ← esta parte es clave
                   .HasForeignKey<Credencial>(c => c.PacienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ObraSocial)
                   .WithMany(o => o.Credenciales)
                   .HasForeignKey(c => c.ObraSocialId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
