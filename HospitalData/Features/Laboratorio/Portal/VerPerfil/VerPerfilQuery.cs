using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Laboratorio.Portal.VerPerfil
{
    public class VerPerfilQuery : IRequest<UserProfileDto>
    {
        public int UserId { get; set; }
        public VerPerfilQuery(int userId) => UserId = userId;
    }
}