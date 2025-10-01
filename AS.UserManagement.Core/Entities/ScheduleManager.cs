using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.UserManagement.Core.Entities
{
    [Table("schedule_manager", Schema = "user_management")]
    public class ScheduleManager
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("first_name")]
        [StringLength(80)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [StringLength(80)]
        public string LastName { get; set; }

        [Required]
        [Column("email")]
        [StringLength(60)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("phone")]
        [StringLength(60)]
        public string? Phone { get; set; }

        [Column("notes")]
        [StringLength(500)]
        public string? Notes { get; set; }

        [Column("photo_url")]
        [StringLength(300)]
        public string? PhotoUrl { get; set; }

        [Column("registration_date")]
        public DateOnly RegistrationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [Column("deactivation_date")]
        public DateOnly? DeactivationDate { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;
    }
}