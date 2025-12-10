using System.ComponentModel.DataAnnotations;

namespace HospitalData.DTOs
{
    public class UserProfileDto
    {
        public int UserID { get; set; }
        public string Role { get; set; } = "";

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        public string Phone { get; set; } = "";
        
        public string? Address { get; set; } 
        
        public string? NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }
    }
}