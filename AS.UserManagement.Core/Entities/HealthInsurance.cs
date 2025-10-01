using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.UserManagement.Core.Entities
{
    [Table("medical_insurance", Schema = "user_management")]
    public class HealthInsurance
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("type")]
        [StringLength(50)]
        public string? Type { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string? Description { get; set; }

        // Navegación
        public virtual ICollection<Patient> AffiliatedPatients { get; set; }
            = new List<Patient>();
    }
}