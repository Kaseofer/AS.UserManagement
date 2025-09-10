using AgendaSaludApp.Core.Configurations;
using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaSaludApp.Infrastructure.Persistence.Context
{
    public class AgendaSaludDBContext : DbContext
    {
        public AgendaSaludDBContext(DbContextOptions<AgendaSaludDBContext> options) : base(options) { }

        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<AgendaCitas> Turnos { get; set; }
        public DbSet<EstadoCita> EstadosTurno { get; set; }
        public DbSet<MotivoCita> Motivos { get; set; }
        public DbSet<ProfesionalHorarios> ProfesionalHorarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ObraSocial> ObrasSociales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar las configuraciones de las entidades

            modelBuilder.ApplyConfiguration(new EspecialidadConfiguration());
            modelBuilder.ApplyConfiguration(new EstadoCitaConfiguration());
            modelBuilder.ApplyConfiguration(new MotivoCitaConfiguration());
            modelBuilder.ApplyConfiguration(new ObraSocialConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new ProfesionalConfiguration());
            modelBuilder.ApplyConfiguration(new ProfesionalHorariosConfiguration());
            modelBuilder.ApplyConfiguration(new AgendaCitasConfiguration());

            // Filtro global para no traer citas vencidas
            modelBuilder.Entity<AgendaCitas>()
            .HasQueryFilter(c => !c.Vencida);
        }
    }
}

