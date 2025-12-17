using MediatR;
using HospitalData.Models;
using HospitalData.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Portal.Recetas
{
    public class VerMisRecetasHandler : IRequestHandler<VerMisRecetasQuery, List<VwPatientActivePrescription>>
    {
        private readonly IPrescriptionManagementService _prescriptionService;

        public VerMisRecetasHandler(IPrescriptionManagementService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        public async Task<List<VwPatientActivePrescription>> Handle(VerMisRecetasQuery request, CancellationToken cancellationToken)
        {
            return await _prescriptionService.GetActivePrescriptionsAsync(request.PatientId);
        }
    }
}