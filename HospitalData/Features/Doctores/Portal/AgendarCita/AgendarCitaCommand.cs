using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Doctores.Portal.AgendarCita
{
    public class AgendarCitaCommand : IRequest<Unit>
    {
        public ScheduleAppointmentDto Dto { get; set; }

        public AgendarCitaCommand(ScheduleAppointmentDto dto)
        {
            Dto = dto;
        }
    }
}