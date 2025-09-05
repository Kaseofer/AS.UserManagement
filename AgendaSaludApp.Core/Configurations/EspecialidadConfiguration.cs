using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class EspecialidadConfiguration : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.ToTable("especialidad", "agendasalud");

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Profesionales)
                   .WithOne(p => p.Especialidad)
                   .HasForeignKey(p => p.EspecialidadId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}