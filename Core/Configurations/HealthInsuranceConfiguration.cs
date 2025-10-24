using AS.UserManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.UserManagement.Core.Configurations
{
    public class HealthInsuranceConfiguration : IEntityTypeConfiguration<HealthInsurance>
    {
        public void Configure(EntityTypeBuilder<HealthInsurance> builder)
        {
            builder.ToTable("medical_insurance", "user_management");

            builder.HasKey(o => o.Id);

            builder.HasMany(c => c.AffiliatedPatients)
                   .WithOne(o => o.HealthInsurance)
                   .HasForeignKey(o => o.HealthInsuranceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}