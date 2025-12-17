using MediatR;
using HospitalData.Models;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Inventario.RegistrarMedicamento
{
    public class RegistrarMedicamentoHandler : IRequestHandler<RegistrarMedicamentoCommand, Unit>
    {
        private readonly HospitalDbContext _context;

        public RegistrarMedicamentoHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RegistrarMedicamentoCommand request, CancellationToken cancellationToken)
        {
            var medication = new Medication
            {
                Name = request.Name,
                Description = request.Description
            };
            
            var inventoryItem = new Inventory
            {
                Medication = medication,  
                Quantity = request.InitialQuantity,
                LastUpdated = DateTime.Now
            };

            _context.Medications.Add(medication);
            _context.Inventories.Add(inventoryItem);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}