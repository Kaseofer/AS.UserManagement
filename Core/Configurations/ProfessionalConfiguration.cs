using AS.UserManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.UserManagement.Core.Configurations
{
    public class ProfessionalConfiguration : IEntityTypeConfiguration<Professional>
    {
        public void Configure(EntityTypeBuilder<Professional> builder)
        {
            // Tabla
            builder.ToTable("professional", "user_management");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Configurar Id como UUID con default
            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.HasOne(p => p.Specialty)
                   .WithMany(e => e.Professionals)
                   .HasForeignKey(p => p.SpecialtyId)
                   .OnDelete(DeleteBehavior.Restrict); // Evitar borrado en cascada

            // Índices únicos
            builder.HasIndex(p => p.Email)
                   .IsUnique()
                   .HasDatabaseName("profesional_email_key");
         
        }
    }
}