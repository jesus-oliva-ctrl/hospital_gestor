using MediatR;

namespace HospitalData.Features.Inventario.ActualizarStock
{
    public class ActualizarStockCommand : IRequest<Unit>
    {
        public int MedicationId { get; set; }
        public int QuantityToAdd { get; set; }
    }
}