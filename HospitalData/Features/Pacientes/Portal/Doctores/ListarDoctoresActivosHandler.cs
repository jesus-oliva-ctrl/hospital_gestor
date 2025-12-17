using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Portal.Doctores
{
    public class ListarDoctoresActivosHandler : IRequestHandler<ListarDoctoresActivosQuery, List<Doctor>>
    {
        private readonly HospitalDbContext _context;

        public ListarDoctoresActivosHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> Handle(ListarDoctoresActivosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Doctors
                                 .Include(d => d.Specialty)
                                 .OrderBy(d => d.LastName)
                                 .ToListAsync(cancellationToken);
        }
    }
}