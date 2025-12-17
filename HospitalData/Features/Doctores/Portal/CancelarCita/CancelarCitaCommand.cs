using MediatR;

namespace HospitalData.Features.Doctores.Portal.CancelarCita
{
    public class CancelarCitaCommand : IRequest<Unit>
    {
        public int AppointmentId { get; set; }
        public CancelarCitaCommand(int appointmentId) => AppointmentId = appointmentId;
    }
}