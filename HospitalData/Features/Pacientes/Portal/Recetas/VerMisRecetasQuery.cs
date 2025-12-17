using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Pacientes.Portal.Recetas
{
    public class VerMisRecetasQuery : IRequest<List<VwPatientActivePrescription>>
    {
        public int PatientId { get; set; }
        public VerMisRecetasQuery(int patientId) => PatientId = patientId;
    }
}