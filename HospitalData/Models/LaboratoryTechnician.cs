using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalData.Models
{
    [Table("LaboratoryTechnicians")] 
    public class LaboratoryTechnician
    {
        [Key]
        [Column("LabTechID")]
        public int LabTechId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }

        [Column("AreaID")]
        public int AreaId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        
        [ForeignKey("AreaId")]
        public virtual LabArea? Area { get; set; } 
    }
}