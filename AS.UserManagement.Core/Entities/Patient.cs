using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.UserManagement.Core.Entities
{
    [Table("patient", Schema = "user_management")]
    public class Patient
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("first_name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [Column("dni")]
        public int Dni { get; set; }

        [Required]
        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }

        [Required]
        [Column("gender")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Column("phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [Column("email")]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("notes")]
        [StringLength(500)]
        public string? Notes { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        [Column("health_insurance_id")]
        public int? HealthInsuranceId { get; set; }

        [Column("member_number")]
        [StringLength(50)]
        public string? MemberNumber { get; set; }

        [Column("plan")]
        [StringLength(100)]
        public string? Plan { get; set; }

        [Column("is_private")]
        public bool IsPrivate { get; set; } = false;

        // Navegación
        [ForeignKey("HealthInsuranceId")]
        public virtual HealthInsurance? HealthInsurance { get; set; }
    }
}