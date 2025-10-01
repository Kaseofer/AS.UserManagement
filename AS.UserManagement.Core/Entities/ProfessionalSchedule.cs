using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.UserManagement.Core.Entities
{
    [Table("professional_schedule", Schema = "user_management")]
    public class ProfessionalSchedule
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("professional_id")]
        public Guid ProfessionalId { get; set; }

        [Required]
        [Column("day_of_week")]
        [Range(0, 6)]
        public int DayOfWeek { get; set; }

        [Required]
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Column("appointment_duration_mins")]
        public int AppointmentDurationMins { get; set; }

        // Navegación
        [ForeignKey("ProfessionalId")]
        public virtual Professional Professional { get; set; }
    }
}