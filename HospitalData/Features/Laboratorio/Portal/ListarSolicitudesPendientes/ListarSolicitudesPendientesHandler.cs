using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.ListarSolicitudesPendientes
{
    public class ListarSolicitudesPendientesHandler : IRequestHandler<ListarSolicitudesPendientesQuery, List<LabRequestDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarSolicitudesPendientesHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<LabRequestDto>> Handle(ListarSolicitudesPendientesQuery request, CancellationToken cancellationToken)
        {
            var sqlQuery = @"
                SELECT 
                    R.RequestID,
                    R.RequestDate,
                    (P.FirstName + ' ' + P.LastName) AS PatientName,
                    (D.FirstName + ' ' + D.LastName) AS DoctorName,
                    T.TestName,
                    R.Status,
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

            return await _context.Database
                .SqlQueryRaw<LabRequestDto>(sqlQuery, request.UserId)
                .ToListAsync(cancellationToken);
        }
    }
}