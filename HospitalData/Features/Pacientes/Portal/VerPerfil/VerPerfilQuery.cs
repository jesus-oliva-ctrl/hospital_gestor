using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Pacientes.Portal.VerPerfil
{
    public class VerPerfilQuery : IRequest<PatientProfileDto>
    {
        public int UserId { get; set; }

        public VerPerfilQuery(int userId)
        {
            UserId = userId;
        }
    }
}