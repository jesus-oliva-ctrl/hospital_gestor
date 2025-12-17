using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Portal.HistorialMedico
{
    public class VerHistorialMedicoHandler : IRequestHandler<VerHistorialMedicoQuery, List<MedicalHistoryDto>>
    {
        private readonly HospitalDbContext _context;

        public VerHistorialMedicoHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalHistoryDto>> Handle(VerHistorialMedicoQuery request, CancellationToken cancellationToken)
        {
            return await _context.MedicalHistories
                .Where(mh => mh.PatientId == request.PatientId)
                .OrderByDescending(mh => mh.VisitDate) 
                .Select(mh => new MedicalHistoryDto
                {
                    HistoryID = mh.HistoryId,
                    VisitDate = mh.VisitDate,
                    Description = mh.Description,
                    PatientFirstName = mh.Patient.FirstName,
                    PatientLastName = mh.Patient.LastName,
                    PatientID = mh.PatientId
                })
                .ToListAsync(cancellationToken);
        }
    }
}