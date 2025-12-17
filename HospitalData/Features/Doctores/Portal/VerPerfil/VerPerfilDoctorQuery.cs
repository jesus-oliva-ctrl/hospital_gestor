using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Doctores.Portal.Perfil
{
    public class VerPerfilDoctorQuery : IRequest<DoctorProfileDto>
    {
        public int UserId { get; set; }
        public VerPerfilDoctorQuery(int userId) => UserId = userId;
    }
}