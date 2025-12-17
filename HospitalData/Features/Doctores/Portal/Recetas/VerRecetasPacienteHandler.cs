using MediatR;
using HospitalData.Services;
using HospitalData.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.Recetas
{
    public class VerRecetasPacienteHandler : IRequestHandler<VerRecetasPacienteQuery, List<Prescription>>
    {
        private readonly IPrescriptionManagementService _prescriptionService;

        public VerRecetasPacienteHandler(IPrescriptionManagementService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        public async Task<List<Prescription>> Handle(VerRecetasPacienteQuery request, CancellationToken cancellationToken)
        {
            return await _prescriptionService.GetPrescriptionsForPatientAsync(request.PatientId);
        }
    }
}