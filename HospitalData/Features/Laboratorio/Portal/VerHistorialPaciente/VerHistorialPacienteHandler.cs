using MediatR;
using HospitalData.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.VerHistorialPaciente
{
    public class VerHistorialPacienteHandler : IRequestHandler<VerHistorialPacienteQuery, List<LabResult>>
    {
        private readonly IMongoCollection<LabResult> _mongoCollection;

        public VerHistorialPacienteHandler(IMongoDatabase mongoDb)
        {
            _mongoCollection = mongoDb.GetCollection<LabResult>("LabResults");
        }

        public async Task<List<LabResult>> Handle(VerHistorialPacienteQuery request, CancellationToken cancellationToken)
        {
            return await _mongoCollection.Find(x => x.PatientId == request.PatientId)
                                         .SortByDescending(x => x.Date)
                                         .ToListAsync(cancellationToken);
        }
    }
}