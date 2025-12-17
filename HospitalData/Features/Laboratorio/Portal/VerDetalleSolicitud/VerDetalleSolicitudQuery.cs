using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Laboratorio.Portal.VerDetalleSolicitud
{
    public class VerDetalleSolicitudQuery : IRequest<LabRequestDto?>
    {
        public int RequestId { get; set; }
        public VerDetalleSolicitudQuery(int requestId) => RequestId = requestId;
    }
}