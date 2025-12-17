using MediatR;
using HospitalData.DTOs;

namespace HospitalData.Features.Doctores.Portal.Recetas
{
    public class CrearRecetaCommand : IRequest<Unit>
    {
        public CreatePrescriptionDto Dto { get; set; }
        public CrearRecetaCommand(CreatePrescriptionDto dto) => Dto = dto;
    }
}