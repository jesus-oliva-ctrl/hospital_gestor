using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalData.DTOs
{
    public class CreatePrescriptionDto
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un medicamento.")]
        public int MedicationID { get; set; }

        [Required(ErrorMessage = "La dosis es obligatoria.")]
        public string Dosage { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int? QuantityToDispense { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime? EndDate { get; set; } = DateTime.Now.AddDays(7);
    }
}  