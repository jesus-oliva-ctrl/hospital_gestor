using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.VerDetalleSolicitud
{
    public class VerDetalleSolicitudHandler : IRequestHandler<VerDetalleSolicitudQuery, LabRequestDto?>
    {
        private readonly HospitalDbContext _context;

        public VerDetalleSolicitudHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<LabRequestDto?> Handle(VerDetalleSolicitudQuery request, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT 
                    R.RequestID, R.RequestDate, R.Status,
                    (P.FirstName + ' ' + P.LastName) AS PatientName,
                    (D.FirstName + ' ' + D.LastName) AS DoctorName,
                    T.TestName,
                    P.PatientID,
                    D.DoctorID
                FROM LabRequests R
                INNER JOIN LabTests T ON R.TestID = T.TestID
                INNER JOIN Appointments A ON R.AppointmentID = A.AppointmentID
                INNER JOIN Patients P ON A.PatientID = P.PatientID
                INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
                WHERE R.RequestID = {0}";

            var result = await _context.Database
                .SqlQueryRaw<LabRequestDto>(sql, request.RequestId)
                .ToListAsync(cancellationToken);

            return result.FirstOrDefault();
        }
    }
}