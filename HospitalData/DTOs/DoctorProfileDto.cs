namespace HospitalData.DTOs
{
    public class DoctorProfileDto : UserProfileBaseDto
    {
        public string SpecialtyName { get; set; } = string.Empty; 
        public int SpecialtyID { get; set; }
    }
}