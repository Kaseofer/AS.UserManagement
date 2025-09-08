using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class ProfesionalHorarioConfiguration : IEntityTypeConfiguration<ProfesionalHorario>
    {
        public void Configure(EntityTypeBuilder<ProfesionalHorario> builder)
        {

            builder.ToTable("profesional_horario", "agendasalud");

            builder.HasKey(ph => ph.Id);

            

                   
        }
    }
}