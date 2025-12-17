using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Staff.VerTableroCitas
{
    public class VerTableroCitasHandler : IRequestHandler<VerTableroCitasQuery, List<AppointmentDetailDto>>
    {
        private readonly HospitalDbContext _context;

        public VerTableroCitasHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentDetailDto>> Handle(VerTableroCitasQuery request, CancellationToken cancellationToken)
        {
                return await _context.StaffAppointmentManagementView
                                .OrderByDescending(a => a.AppointmentDate)
                                .ToListAsync(cancellationToken);
        }
    }
}