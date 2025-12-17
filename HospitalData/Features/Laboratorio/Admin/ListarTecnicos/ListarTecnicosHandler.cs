using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Admin.ListarTecnicos
{
    public class ListarTecnicosHandler : IRequestHandler<ListarTecnicosQuery, List<LabTechnicianDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarTecnicosHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<LabTechnicianDto>> Handle(ListarTecnicosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Database
                .SqlQueryRaw<LabTechnicianDto>("EXEC SP_GetAllLabTechnicians")
                .ToListAsync(cancellationToken);
        }
    }
}