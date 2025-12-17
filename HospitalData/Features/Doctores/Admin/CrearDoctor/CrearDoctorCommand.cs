using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HospitalWeb.Features.Doctores.Admin.CrearDoctor
{
    public class CrearDoctorCommand : IRequest<Unit> 
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "La especialidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad válida")]
        public int SpecialtyID { get; set; }
    }
}