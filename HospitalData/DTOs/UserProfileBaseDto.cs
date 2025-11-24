using System.ComponentModel.DataAnnotations;

namespace HospitalData.DTOs
{
    public abstract class UserProfileBaseDto
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; } = string.Empty;
        
        public string? NewPassword { get; set; }
    }
}