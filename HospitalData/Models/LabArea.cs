using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class LabArea
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = null!;
        
        public string? Description { get; set; }

        public virtual ICollection<LaboratoryTechnician> LaboratoryTechnicians { get; set; } = new List<LaboratoryTechnician>();
    }
}