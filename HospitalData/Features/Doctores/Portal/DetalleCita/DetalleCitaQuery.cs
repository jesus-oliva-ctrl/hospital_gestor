using MediatR;
using HospitalData.Models;

namespace HospitalData.Features.Doctores.Portal.DetalleCita
{
    public class DetalleCitaQuery : IRequest<Appointment?>
    {
        public int AppointmentId { get; set; }
        public DetalleCitaQuery(int appointmentId) => AppointmentId = appointmentId;
    }
}