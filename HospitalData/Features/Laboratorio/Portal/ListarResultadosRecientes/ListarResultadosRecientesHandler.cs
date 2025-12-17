using MediatR;
using HospitalData.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.ListarResultadosRecientes
{
    public class ListarResultadosRecientesHandler : IRequestHandler<ListarResultadosRecientesQuery, List<LabResult>>
    {
        private readonly IMongoCollection<LabResult> _collection;

        public ListarResultadosRecientesHandler(IMongoDatabase mongoDb)
        {
            _collection = mongoDb.GetCollection<LabResult>("LabResults");
        }

        public async Task<List<LabResult>> Handle(ListarResultadosRecientesQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(_ => true)
                                    .SortByDescending(x => x.Date)
                                    .Limit(100)
                                    .ToListAsync(cancellationToken);
        }
    }
}