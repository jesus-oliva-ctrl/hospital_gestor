using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Inventario.ActualizarStock
{
    public class ActualizarStockHandler : IRequestHandler<ActualizarStockCommand, Unit>
    {
        private readonly HospitalDbContext _context;

        public ActualizarStockHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ActualizarStockCommand request, CancellationToken cancellationToken)
        {
            var inventoryItem = await _context.Inventories
                .FirstOrDefaultAsync(i => i.MedicationId == request.MedicationId, cancellationToken);

            if (inventoryItem != null)
            {
                inventoryItem.Quantity += request.QuantityToAdd;
                inventoryItem.LastUpdated = DateTime.Now;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("El medicamento no fue encontrado en el inventario.");
            }

            return Unit.Value;
        }
    }
}