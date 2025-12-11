using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalData.Models
{
    [Table("LabAreas")]
    public class LabArea
    {
        [Key]
        [Column("AreaID")]
        public int AreaId { get; set; }

        [Required]
        [StringLength(100)]
        public string AreaName { get; set; } = string.Empty;
        public virtual ICollection<LaboratoryTechnician> LaboratoryTechnicians { get; set; } = new List<LaboratoryTechnician>();
    }
}