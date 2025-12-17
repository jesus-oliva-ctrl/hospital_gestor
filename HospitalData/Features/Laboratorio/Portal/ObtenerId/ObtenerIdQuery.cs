using MediatR;

namespace HospitalData.Features.Laboratorio.Portal.ObtenerId
{
    public class ObtenerIdQuery : IRequest<int>
    {
        public int UserId { get; set; }
        public ObtenerIdQuery(int userId) => UserId = userId;
    }
}