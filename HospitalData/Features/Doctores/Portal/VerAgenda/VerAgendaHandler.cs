using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.VerAgenda
{
    public class VerAgendaHandler : IRequestHandler<VerAgendaQuery, List<VwDoctorAgendaSummary>>
    {
        private readonly HospitalDbContext _context;

        public VerAgendaHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<VwDoctorAgendaSummary>> Handle(VerAgendaQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                                    .FirstOrDefaultAsync(d => d.UserId == request.UserId, cancellationToken);

            if (doctor == null)
            {
                return new List<VwDoctorAgendaSummary>();
            }

            int correctDoctorId = doctor.DoctorId;

            return await _context.VwDoctorAgendaSummaries
                                .Where(cita => cita.DoctorId == correctDoctorId)
                                .OrderBy(cita => cita.AppointmentDate)
                                .ToListAsync(cancellationToken);
        }
    }
}