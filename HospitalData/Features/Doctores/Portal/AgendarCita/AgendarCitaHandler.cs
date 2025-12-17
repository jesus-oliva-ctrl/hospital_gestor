using MediatR;
using HospitalData.Services; 
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.AgendarCita
{
    public class AgendarCitaHandler : IRequestHandler<AgendarCitaCommand, Unit>
    {
        private readonly IAppointmentManagementService _appointmentService;

        public AgendarCitaHandler(IAppointmentManagementService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Unit> Handle(AgendarCitaCommand request, CancellationToken cancellationToken)
        {
            await _appointmentService.ScheduleAppointmentAsync(request.Dto);
            return Unit.Value;
        }
    }
}