using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Pacientes.Portal.HistorialMedico
{
    public class VerHistorialMedicoQuery : IRequest<List<MedicalHistoryDto>>
    {
        public int PatientId { get; set; }
        public VerHistorialMedicoQuery(int patientId) => PatientId = patientId;
    }
}