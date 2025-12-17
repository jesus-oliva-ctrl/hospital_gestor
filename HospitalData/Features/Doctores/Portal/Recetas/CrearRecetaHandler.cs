using MediatR;
using HospitalData.Services; 
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.Recetas
{
    public class CrearRecetaHandler : IRequestHandler<CrearRecetaCommand, Unit>
    {
        private readonly IPrescriptionManagementService _prescriptionService;

        public CrearRecetaHandler(IPrescriptionManagementService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        public async Task<Unit> Handle(CrearRecetaCommand request, CancellationToken cancellationToken)
        {
            await _prescriptionService.CreatePrescriptionAsync(request.Dto);
            return Unit.Value;
        }
    }
}