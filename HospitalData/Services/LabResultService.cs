using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class LabResultService : ILabResultService
    {
        private readonly IMongoCollection<LabResult> _collection; 
        private readonly HospitalDbContext _sqlContext;         

        public LabResultService(IConfiguration config, HospitalDbContext sqlContext)
        {
            _sqlContext = sqlContext;

            var connectionString = config.GetConnectionString("MongoConnection");
            
            var databaseName = config["MongoDbSettings:DatabaseName"] ?? 
                               config["MongoSettings:DatabaseName"] ?? 
                               "HospitalLabDB";
                               
            var collectionName = config["MongoDbSettings:CollectionName"] ?? 
                                 config["MongoSettings:CollectionName"] ?? 
                                 "LabResults";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<LabResult>(collectionName);
        }


        public async Task CreateResultAsync(LabResult result)
        {
            await _collection.InsertOneAsync(result);
        }

        public async Task<LabResult> GetResultByRequestIdAsync(int requestId)
        {
            return await _collection.Find(x => x.RequestId == requestId).FirstOrDefaultAsync();
        }

        public async Task<List<LabResult>> GetHistoryByPatientIdAsync(int patientId)
        {
            return await _collection.Find(x => x.PatientId == patientId)
                                    .SortByDescending(x => x.Date)
                                    .ToListAsync();
        }


        public async Task<List<LabRequestDto>> GetPendingRequestsForUserAsync(int userId)
        {
            var sqlQuery = @"
                SELECT 
                    R.RequestID,
                    R.RequestDate,
                    (P.FirstName + ' ' + P.LastName) AS PatientName,
                    (D.FirstName + ' ' + D.LastName) AS DoctorName,
                    T.TestName,
                    R.Status,
                    -- AGREGAMOS ESTAS DOS COLUMNAS FALTANTES:
                    P.PatientID,
                    D.DoctorID
                FROM LabRequests R
                INNER JOIN LabTests T ON R.TestID = T.TestID
                INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                INNER JOIN Patients P ON A.PatientID = P.PatientID
                INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
                INNER JOIN LaboratoryTechnicians LT ON LT.UserID = {0} 
                WHERE 
                    R.Status = 'Pendiente'
                    AND T.AreaID = LT.AreaID 
                ORDER BY 
                    R.RequestDate ASC";

            var requests = await _sqlContext.Database
                .SqlQueryRaw<LabRequestDto>(sqlQuery, userId)
                .ToListAsync();

            return requests;
        }

        public async Task<LabRequestDto?> GetRequestByIdAsync(int requestId)
        {
            var sql = @"
                SELECT 
                    R.RequestID, R.RequestDate, R.Status,
                    (P.FirstName + ' ' + P.LastName) AS PatientName,
                    (D.FirstName + ' ' + D.LastName) AS DoctorName,
                    T.TestName,
                    P.PatientID,   -- Necesario para Mongo
                    D.DoctorID     -- Necesario para Mongo
                FROM LabRequests R
                INNER JOIN LabTests T ON R.TestID = T.TestID
                INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                INNER JOIN Patients P ON A.PatientID = P.PatientID
                INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
                WHERE R.RequestID = {0}";

            var result = await _sqlContext.Database
                .SqlQueryRaw<LabRequestDto>(sql, requestId)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task CompleteRequestAsync(int requestId)
        {
            await _sqlContext.Database.ExecuteSqlRawAsync(
                "UPDATE LabRequests SET Status = 'Finalizado' WHERE RequestID = {0}", requestId);

            var sqlUpdateCita = @"
                UPDATE Appointments 
                SET Status = 'Resultados Listos' 
                WHERE AppointmentID = (SELECT AppointmentID FROM LabRequests WHERE RequestID = {0})";
            
            await _sqlContext.Database.ExecuteSqlRawAsync(sqlUpdateCita, requestId);    
        
        }

        public async Task<int> GetLabTechIdByUserIdAsync(int userId)
        {
            var result = await _sqlContext.Database
                .SqlQueryRaw<int>("SELECT LabTechID AS Value FROM LaboratoryTechnicians WHERE UserID = {0}", userId)
                .FirstOrDefaultAsync();
            
            return result;
            return result;
        }
        public async Task<List<LabResult>> GetAllRecentResultsAsync()
        {
            return await _collection.Find(_ => true)
                                    .SortByDescending(x => x.Date)
                                    .Limit(100)
                                    .ToListAsync();
        }
    }
}