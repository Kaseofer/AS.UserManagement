using AS.UserManagement.Core.Configurations;
using AS.UserManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AS.UserManagement.Infrastructure.Persistence.Context
{
    public class AgendaSaludDBContext : DbContext
    {
        public AgendaSaludDBContext(DbContextOptions<AgendaSaludDBContext> options) : base(options) { }

        public DbSet<Professional> Profesionales { get; set; }
        public DbSet<MedicalSpecialty> Especialidades { get; set; }
        public DbSet<Patient> Pacientes { get; set; }
        public DbSet<HealthInsurance> ObrasSociales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar las configuraciones de las entidades

            modelBuilder.ApplyConfiguration(new MedicalSpecialtyConfiguration());

            modelBuilder.ApplyConfiguration(new HealthInsuranceConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessionalConfiguration());


        }
    }
}

