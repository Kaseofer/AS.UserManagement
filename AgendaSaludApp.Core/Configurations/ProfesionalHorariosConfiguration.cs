using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class ProfesionalHorariosConfiguration : IEntityTypeConfiguration<ProfesionalHorarios>
    {
        public void Configure(EntityTypeBuilder<ProfesionalHorarios> builder)
        {

            builder.ToTable("profesional_horarios", "agendasalud");

            builder.HasKey(ph => ph.Id);

           
            

                   
        }
    }
}