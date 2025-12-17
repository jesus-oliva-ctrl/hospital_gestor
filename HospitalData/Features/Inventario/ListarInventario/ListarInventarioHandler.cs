using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Inventario.ListarInventario
{
    public class ListarInventarioHandler : IRequestHandler<ListarInventarioQuery, List<InventoryDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarInventarioHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryDto>> Handle(ListarInventarioQuery request, CancellationToken cancellationToken)
        {
            return await _context.Medications
                .Join(_context.Inventories,
                      med => med.MedicationId,
                      inv => inv.MedicationId,
                      (med, inv) => new InventoryDto
                      {
                          MedicationId = med.MedicationId,
                          Name = med.Name,
                          Description = med.Description,
                          Quantity = inv.Quantity,
                          LastUpdated = inv.LastUpdated ?? DateTime.MinValue
                      })
                .OrderBy(m => m.Name)
                .ToListAsync(cancellationToken);
        }
    }
}