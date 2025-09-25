using AS.UserManegement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.UserManegement.Core.Configurations
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