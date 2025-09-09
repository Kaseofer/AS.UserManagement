using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class ProfesionalConfiguration : IEntityTypeConfiguration<Profesional>
    {
        public void Configure(EntityTypeBuilder<Profesional> builder)
        {
            builder.ToTable("profesional", "agendasalud");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Especialidad)
                   .WithMany(e => e.Profesionales)
                   .HasForeignKey(p => p.EspecialidadId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Horarios)
                   .WithOne(h => h.Profesional)
                   .HasForeignKey(h => h.ProfesionalId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Citas)
                     .WithOne(c => c.Profesional)
                     .HasForeignKey(c => c.ProfesionalId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}