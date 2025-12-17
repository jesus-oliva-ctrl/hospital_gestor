using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Pacientes.Portal.Citas
{
    public class VerMisCitasQuery : IRequest<List<VwPatientAppointment>>
    {
        public int PatientId { get; set; }
        public VerMisCitasQuery(int patientId) => PatientId = patientId;
    }
}