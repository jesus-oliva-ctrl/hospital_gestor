using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Portal.Citas
{
    public class VerMisCitasHandler : IRequestHandler<VerMisCitasQuery, List<VwPatientAppointment>>
    {
        private readonly HospitalDbContext _context;

        public VerMisCitasHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<VwPatientAppointment>> Handle(VerMisCitasQuery request, CancellationToken cancellationToken)
        {
            return await _context.VwPatientAppointments
                         .Where(a => a.PatientId == request.PatientId)
                         .OrderBy(a => a.AppointmentDate)
                         .ToListAsync(cancellationToken);
        }
    }
}