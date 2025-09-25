using AS.UserManegement.Core.Configurations;
using AS.UserManegement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AS.UserManegement.Infrastructure.Persistence.Context
{
    public class AgendaSaludDBContext : DbContext
    {
        public AgendaSaludDBContext(DbContextOptions<AgendaSaludDBContext> options) : base(options) { }

        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<ProfesionalHorarios> ProfesionalHorarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ObraSocial> ObrasSociales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar las configuraciones de las entidades

            modelBuilder.ApplyConfiguration(new EspecialidadConfiguration());

            modelBuilder.ApplyConfiguration(new ObraSocialConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new ProfesionalConfiguration());
            modelBuilder.ApplyConfiguration(new ProfesionalHorariosConfiguration());


        }
    }
}

