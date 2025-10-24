using AS.UserManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.UserManagement.Core.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patient", "user_management");

            builder.HasKey(p => p.Id);
            // Configurar Id como UUID con default
            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .HasDefaultValueSql("gen_random_uuid()");

            // Configurar la relación uno a uno entre Paciente y Credencial

            builder.HasOne(p => p.HealthInsurance)
                .WithMany(c => c.AffiliatedPatients)
                .HasForeignKey(c => c.HealthInsuranceId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}