using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class TurnoConfiguration : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            builder.ToTable("turno", "agendasalud"); 

            builder.HasOne(t => t.Profesional)
                   .WithMany(p => p.Turnos)
                   .HasForeignKey(t => t.ProfesionalId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(t => t.Detalle)
                .WithOne(d => d.Turno)
                .HasForeignKey<TurnoDetalle>(d => d.TurnoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}