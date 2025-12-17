using MediatR;
using System;

namespace HospitalData.Features.Pacientes.Portal.ActualizarPerfil
{
    public class ActualizarPerfilCommand : IRequest<Unit>
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Address { get; set; }
        
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}