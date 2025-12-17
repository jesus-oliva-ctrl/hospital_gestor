using MediatR;
using HospitalData.Services;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Portal.Citas
{
    public class CancelarCitaHandler : IRequestHandler<CancelarCitaCommand, Unit>
    {
        private readonly IAppointmentManagementService _appointmentService;

        public CancelarCitaHandler(IAppointmentManagementService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Unit> Handle(CancelarCitaCommand request, CancellationToken cancellationToken)
        {
            await _appointmentService.CancelAppointmentAsync(request.AppointmentId);
            return Unit.Value;
        }
    }
}