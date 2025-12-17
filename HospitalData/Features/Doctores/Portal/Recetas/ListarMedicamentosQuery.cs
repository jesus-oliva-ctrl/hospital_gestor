using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Doctores.Portal.Recetas
{
    public class ListarMedicamentosQuery : IRequest<List<InventoryDto>> { }
}