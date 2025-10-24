using AS.UserManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.UserManagement.Core.Configurations
{
    public class ScheduleManagerConfiguration : IEntityTypeConfiguration<ScheduleManager>
    {
        public void Configure(EntityTypeBuilder<ScheduleManager> builder)
        {
            builder.ToTable("schedule_manager", "user_management");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.FirstName)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(s => s.LastName)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(s => s.Phone)
                   .HasMaxLength(60);

            builder.Property(s => s.Notes)
                   .HasMaxLength(200);

            builder.Property(s => s.Email)
                   .HasMaxLength(60);

            builder.Property(s => s.PhotoUrl)
                   .HasMaxLength(300);

            builder.Property(s => s.RegistrationDate)
                   .HasDefaultValueSql("CURRENT_DATE")
                   .IsRequired();

            builder.Property(s => s.Active)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.HasIndex(s => s.Email)
                   .IsUnique()
                   .HasDatabaseName("schedule_manager_email_key");

            builder.HasIndex(s => s.Active)
                   .HasDatabaseName("idx_schedule_manager_activo");
        }
    }
}