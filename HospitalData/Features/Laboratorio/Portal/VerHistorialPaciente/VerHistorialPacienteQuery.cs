using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Laboratorio.Portal.VerHistorialPaciente
{
    public class VerHistorialPacienteQuery : IRequest<List<LabResult>>
    {
        public int PatientId { get; set; }
        public VerHistorialPacienteQuery(int patientId) => PatientId = patientId;
    }
}