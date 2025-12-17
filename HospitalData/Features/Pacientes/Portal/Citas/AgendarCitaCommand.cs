using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Pacientes.Portal.AgendarCita
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