using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Doctores.Portal.Recetas
{
    public class VerRecetasPacienteQuery : IRequest<List<Prescription>>
    {
        public int PatientId { get; set; }
        public VerRecetasPacienteQuery(int patientId) => PatientId = patientId;
    }
}