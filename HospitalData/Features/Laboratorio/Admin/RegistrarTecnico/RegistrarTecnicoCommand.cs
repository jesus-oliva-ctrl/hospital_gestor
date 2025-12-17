using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HospitalData.Features.Laboratorio.Admin.RegistrarTecnico
{
    public class RegistrarTecnicoCommand : IRequest<Unit>
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un área")]
        public int AreaID { get; set; }
    }
}