namespace HospitalData.DTOs
{
    public class ModifyMedicationDto
    {
        public int MedicationId { get; set; }
        
        public string? Name { get; set; } 
        public string? Description { get; set; }
        
        public int? QuantityAdjustment { get; set; } 
    }
}