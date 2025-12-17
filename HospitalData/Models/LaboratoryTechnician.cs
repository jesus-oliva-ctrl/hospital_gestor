using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class LaboratoryTechnician
    {
        public int LabTechId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public int AreaId { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual LabArea? Area { get; set; }
    }
}