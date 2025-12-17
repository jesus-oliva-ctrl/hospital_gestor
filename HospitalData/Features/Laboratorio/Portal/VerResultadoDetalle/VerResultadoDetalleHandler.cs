using MediatR;
using HospitalData.Models;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.VerResultadoDetalle
{
    public class VerResultadoDetalleHandler : IRequestHandler<VerResultadoDetalleQuery, LabResult?>
    {
        private readonly IMongoCollection<LabResult> _collection;

        public VerResultadoDetalleHandler(IMongoDatabase mongoDb)
        {
            _collection = mongoDb.GetCollection<LabResult>("LabResults");
        }

        public async Task<LabResult?> Handle(VerResultadoDetalleQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.RequestId == request.RequestId)
                                    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}