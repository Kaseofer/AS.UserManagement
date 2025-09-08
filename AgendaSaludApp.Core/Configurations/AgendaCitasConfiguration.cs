using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class AgendaCitasConfiguration : IEntityTypeConfiguration<AgendaCitas>
    {
        public void Configure(EntityTypeBuilder<AgendaCitas> builder)
        {
            builder.ToTable("agendacitas", "agendasalud");

            builder.HasKey(a => a.Id);


            builder.HasOne(t => t.Profesional)
                   .WithMany(p => p.Citas)
                   .HasForeignKey(t => t.ProfesionalId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(t => t.Paciente)
                   .WithMany(p => p.Citas)
                   .HasForeignKey(t => t.PacienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.MotivoCita)
                   .WithMany()
                   .HasForeignKey(t => t.MotivoCitaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.EstadoCita)
                   .WithMany()
                   .HasForeignKey(t => t.EstadoCitaId)
                   .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}