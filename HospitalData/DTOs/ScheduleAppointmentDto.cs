using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalData.DTOs
{
    public class ScheduleAppointmentDto
    {
        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
        public DateTime AppointmentDate { get; set; }
    }
}