using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.UserManagement.Core.Entities
{
    [Table("professional", Schema = "user_management")]
    public class Professional
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("license_number")]
        [StringLength(20)]
        public string LicenseNumber { get; set; }

        [Required]
        [Column("first_name")]
        [StringLength(80)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [StringLength(80)]
        public string LastName { get; set; }

        [Required]
        [Column("specialty_id")]
        public int SpecialtyId { get; set; }

        [Column("email")]
        [StringLength(60)]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("phone")]
        [StringLength(60)]
        public string? Phone { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Column("photo_url")]
        [StringLength(300)]
        public string? PhotoUrl { get; set; }

        [Column("phone_2")]
        [StringLength(60)]
        public string? Phone2 { get; set; }

        [Column("notes")]
        [StringLength(100)]
        public string? Notes { get; set; }

        [Column("deactivation_date")]
        public DateOnly? DeactivationDate { get; set; }

        // Navegaciones
        public virtual ICollection<ProfessionalSchedule> Schedules { get; set; }
            = new List<ProfessionalSchedule>();

        public virtual MedicalSpecialty Specialty { get; set; }
    }
}