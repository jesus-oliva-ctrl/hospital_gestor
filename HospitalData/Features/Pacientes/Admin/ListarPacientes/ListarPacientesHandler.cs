using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Admin.ListarPacientes
{
    public class ListarPacientesHandler : IRequestHandler<ListarPacientesQuery, List<Patient>>
    {
        private readonly HospitalDbContext _context;

        public ListarPacientesHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> Handle(ListarPacientesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Patients
                .OrderBy(p => p.LastName)
                .ToListAsync(cancellationToken);
        }
    }
}