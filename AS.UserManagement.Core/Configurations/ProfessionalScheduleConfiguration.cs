using AS.UserManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.UserManagement.Core.Configurations
{
    public class ProfessionalScheduleConfiguration : IEntityTypeConfiguration<ProfessionalSchedule>
    {
        public void Configure(EntityTypeBuilder<ProfessionalSchedule> builder)
        {

            builder.ToTable("professional_schedule", "user_management");

            builder.HasKey(ph => ph.Id);

           
            

                   
        }
    }
}