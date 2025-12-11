using System.ComponentModel.DataAnnotations;

namespace HospitalData.DTOs
{
    public class PatientProfileDto : UserProfileBaseDto
    {        
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public string? Gender { get; set; }
        public int PatientId { get; set; } 
        public DateOnly? DOB { get; set; }  
    }
}