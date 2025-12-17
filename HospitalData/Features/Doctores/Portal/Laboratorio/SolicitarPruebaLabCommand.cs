using MediatR;

namespace HospitalData.Features.Doctores.Portal.Laboratorio
{
    public class SolicitarPruebaLabCommand : IRequest<Unit>
    {
        public int AppointmentId { get; set; }
        public int TestId { get; set; }
    }
}