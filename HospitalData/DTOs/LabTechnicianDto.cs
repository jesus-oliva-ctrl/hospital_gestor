namespace HospitalData.DTOs
{
    public class LabTechnicianDto
    {
        public int LabTechID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Username { get; set; } = "";
        public string Phone { get; set; } = "";
        public int AreaID { get; set; }
        public string AreaName { get; set; } = "";
    }
}