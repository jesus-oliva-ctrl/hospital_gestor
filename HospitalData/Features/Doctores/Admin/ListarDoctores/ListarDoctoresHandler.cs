using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalWeb.Features.Doctores.Admin.ListarDoctores
{
    public class ListarDoctoresHandler : IRequestHandler<ListarDoctoresQuery, List<Doctor>>
    {
        private readonly HospitalDbContext _context;

        public ListarDoctoresHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> Handle(ListarDoctoresQuery request, CancellationToken cancellationToken)
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .OrderBy(d => d.LastName)
                .ToListAsync(cancellationToken);
        }
    }
}