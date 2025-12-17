using MediatR;

namespace HospitalData.Features.Pacientes.Portal.ObtenerId
{
    public class ObtenerIdPacienteQuery : IRequest<int>
    {
        public int UserId { get; set; }
        public ObtenerIdPacienteQuery(int userId) => UserId = userId;
    }
}