using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HospitalData.Features.Doctores.Portal.Perfil
{
    public class ActualizarPerfilDoctorCommand : IRequest<Unit>
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Username { get; set; } = string.Empty;
        
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}