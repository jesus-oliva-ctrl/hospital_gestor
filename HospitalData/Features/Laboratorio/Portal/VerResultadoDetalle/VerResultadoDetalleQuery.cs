using MediatR;
using HospitalData.Models;

namespace HospitalData.Features.Laboratorio.Portal.VerResultadoDetalle
{
    public class VerResultadoDetalleQuery : IRequest<LabResult?>
    {
        public int RequestId { get; set; }
        public VerResultadoDetalleQuery(int requestId) => RequestId = requestId;
    }
}