namespace HospitalData.DTOs
{
    public class LabTestDto
    {
        public int TestId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public string AreaName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}