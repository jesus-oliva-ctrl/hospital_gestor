using System;

namespace HospitalData.DTOs
{
    public class LabRequestDto
    {
        public int RequestID { get; set; }
        public DateTime RequestDate { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
    }
}