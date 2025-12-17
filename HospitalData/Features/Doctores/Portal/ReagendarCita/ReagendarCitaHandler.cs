using MediatR;
using HospitalData.Services;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.ReagendarCita
{
    public class ReagendarCitaHandler : IRequestHandler<ReagendarCitaCommand, Unit>
    {
        private readonly IAppointmentManagementService _appointmentService;

        public ReagendarCitaHandler(IAppointmentManagementService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Unit> Handle(ReagendarCitaCommand request, CancellationToken cancellationToken)
        {
            await _appointmentService.CancelAppointmentAsync(request.OldAppointmentId);
            await _appointmentService.ScheduleAppointmentAsync(request.NewAppointmentDto);
            
            return Unit.Value;
        }
    }
}