using MediatR;
using HospitalData.DTOs; 
using System.ComponentModel.DataAnnotations;

namespace HospitalData.Features.Inventario.RegistrarMedicamento
{
    public class RegistrarMedicamentoCommand : IRequest<Unit>
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Range(0, int.MaxValue)]
        public int InitialQuantity { get; set; }
    }
}