using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.UserManagement.Core.Entities
{
    [Table("medical_specialty", Schema = "user_management")]
    public class MedicalSpecialty
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("short_name")]
        [StringLength(50)]
        public string? ShortName { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Column("image_url")]
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        // Navegación
        public virtual ICollection<Professional> Professionals { get; set; }
            = new List<Professional>();
    }
}