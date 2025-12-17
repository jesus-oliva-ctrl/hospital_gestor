using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Inventario.ListarInventario
{
    public class ListarInventarioQuery : IRequest<List<InventoryDto>> { }
}