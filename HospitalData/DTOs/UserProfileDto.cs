using System.ComponentModel.DataAnnotations;

namespace HospitalData.DTOs
{
    public class UserProfileDto : UserProfileBaseDto
    {
        public string Role { get; set; } = "";
        
        public string? Address { get; set; } 
        
        [Compare(nameof(NewPassword), ErrorMessage = "las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }
    }
}