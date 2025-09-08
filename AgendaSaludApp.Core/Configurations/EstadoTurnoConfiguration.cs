using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class EstadoTurnoConfiguration : IEntityTypeConfiguration<EstadoCita>
    {
        public void Configure(EntityTypeBuilder<EstadoCita> builder)
        {
            builder.ToTable("estado_turno", "agendasalud");

            builder.HasKey(e => e.Id);
            


        }
    }
}