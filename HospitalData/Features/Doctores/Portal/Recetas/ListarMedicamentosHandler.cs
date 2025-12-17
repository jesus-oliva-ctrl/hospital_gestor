using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.Recetas
{
    public class ListarMedicamentosHandler : IRequestHandler<ListarMedicamentosQuery, List<InventoryDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarMedicamentosHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryDto>> Handle(ListarMedicamentosQuery request, CancellationToken cancellationToken)
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
                .Where(inv => inv.Quantity > 0)
                .OrderBy(m => m.Name)
                .ToListAsync(cancellationToken);
        }
    }
}