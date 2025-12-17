using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HospitalData.Features.Doctores.Portal.CompletarCita
{
    public class CompletarCitaCommand : IRequest<Unit>
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Las notas de diagnóstico son obligatorias")]
        public string DiagnosisNotes { get; set; } = string.Empty;
    }
}