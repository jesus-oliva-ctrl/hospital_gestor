using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.DetalleCita
{
    public class DetalleCitaHandler : IRequestHandler<DetalleCitaQuery, Appointment?>
    {
        private readonly HospitalDbContext _context;

        public DetalleCitaHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> Handle(DetalleCitaQuery request, CancellationToken cancellationToken)
        {
            return await _context.Appointments
                                .Include(a => a.Patient)
                                .FirstOrDefaultAsync(a => a.AppointmentId == request.AppointmentId, cancellationToken);
        }
    }
}