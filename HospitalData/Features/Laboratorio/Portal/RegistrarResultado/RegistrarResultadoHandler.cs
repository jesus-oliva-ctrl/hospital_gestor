using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.RegistrarResultado
{
    public class RegistrarResultadoHandler : IRequestHandler<RegistrarResultadoCommand, Unit>
    {
        private readonly HospitalDbContext _sqlContext;
        private readonly IMongoCollection<LabResult> _mongoCollection;

        public RegistrarResultadoHandler(HospitalDbContext sqlContext, IMongoDatabase mongoDb)
        {
            _sqlContext = sqlContext;
            _mongoCollection = mongoDb.GetCollection<LabResult>("LabResults");
        }

        public async Task<Unit> Handle(RegistrarResultadoCommand request, CancellationToken cancellationToken)
        {
            await _mongoCollection.InsertOneAsync(request.Resultado, cancellationToken: cancellationToken);

            await _sqlContext.Database.ExecuteSqlRawAsync(
                "UPDATE LabRequests SET Status = 'Finalizado' WHERE RequestID = {0}", 
                request.Resultado.RequestId, cancellationToken);


            var sqlUpdateCita = @"
                UPDATE Appointments 
                SET Status = 'Resultados Listos' 
                WHERE AppointmentID = (SELECT AppointmentID FROM LabRequests WHERE RequestID = {0})";
            
            await _sqlContext.Database.ExecuteSqlRawAsync(sqlUpdateCita, request.Resultado.RequestId, cancellationToken);

            return Unit.Value;
        }
    }
}