using System;
using System.Collections.Generic;

namespace HospitalData.Models;

public partial class VwPatientAppointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = null!;

    public string DoctorFirstName { get; set; } = null!;

    public string DoctorLastName { get; set; } = null!;

    public string DoctorSpecialty { get; set; } = null!;
}