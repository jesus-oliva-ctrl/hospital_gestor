using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalWeb.Features.Doctores.Admin.ListarEspecialidades
{
    public class ListarEspecialidadesHandler : IRequestHandler<ListarEspecialidadesQuery, List<SpecialtyDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarEspecialidadesHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<SpecialtyDto>> Handle(ListarEspecialidadesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Specialties
                .Select(s => new SpecialtyDto
                {
                    SpecialtyId = s.SpecialtyId,
                    SpecialtyName = s.SpecialtyName
                })
                .OrderBy(s => s.SpecialtyName)
                .ToListAsync(cancellationToken);
        }
    }
}