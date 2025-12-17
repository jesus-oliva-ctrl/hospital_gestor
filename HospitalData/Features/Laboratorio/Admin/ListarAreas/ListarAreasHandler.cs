using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Admin.ListarAreas
{
    public class ListarAreasHandler : IRequestHandler<ListarAreasQuery, List<LabAreaDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarAreasHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<LabAreaDto>> Handle(ListarAreasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Database
                .SqlQueryRaw<LabAreaDto>("SELECT AreaID, AreaName FROM LabAreas")
                .ToListAsync(cancellationToken);
        }
    }
}