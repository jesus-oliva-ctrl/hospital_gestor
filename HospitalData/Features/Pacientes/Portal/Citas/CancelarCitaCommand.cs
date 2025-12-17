using MediatR;

namespace HospitalData.Features.Pacientes.Portal.Citas
{
    public class CancelarCitaCommand : IRequest<Unit>
    {
        public int AppointmentId { get; set; }
        public CancelarCitaCommand(int appointmentId) => AppointmentId = appointmentId;
    }
}