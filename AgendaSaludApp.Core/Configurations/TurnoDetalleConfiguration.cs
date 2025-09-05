using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class TurnoDetalleConfiguration : IEntityTypeConfiguration<TurnoDetalle>
    {
        public void Configure(EntityTypeBuilder<TurnoDetalle> builder)
        {
            builder.ToTable("turno_detalle", "agendasalud");

            builder.HasKey(td => td.Id);

            builder.HasOne(td => td.Turno)
                   .WithOne(t => t.Detalle)
                     .HasForeignKey<TurnoDetalle>(td => td.TurnoId);

            builder.HasOne(td => td.Paciente)
                   .WithOne()             
                   .HasForeignKey<TurnoDetalle>(td => td.PacienteId);

            builder.HasOne(td => td.Motivo)
                   .WithMany()
                   .HasForeignKey(td => td.MotivoId);





        }
    }
}