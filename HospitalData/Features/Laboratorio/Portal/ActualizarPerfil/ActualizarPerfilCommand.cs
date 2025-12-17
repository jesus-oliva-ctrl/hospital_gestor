using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HospitalData.Features.Laboratorio.Portal.ActualizarPerfil
{
    public class ActualizarPerfilCommand : IRequest<Unit>
    {
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Email { get; set; } = string.Empty;
        
        public string Phone { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}