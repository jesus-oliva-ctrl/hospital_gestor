using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Doctores.Portal.ReagendarCita
{
    public class ReagendarCitaCommand : IRequest<Unit>
    {
        public int OldAppointmentId { get; set; }
        public ScheduleAppointmentDto NewAppointmentDto { get; set; }

        public ReagendarCitaCommand(int oldId, ScheduleAppointmentDto newDto)
        {
            OldAppointmentId = oldId;
            NewAppointmentDto = newDto;
        }
    }
}