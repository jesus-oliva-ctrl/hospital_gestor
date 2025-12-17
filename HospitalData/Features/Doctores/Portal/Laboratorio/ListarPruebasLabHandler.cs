using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.Laboratorio
{
    public class ListarPruebasLabHandler : IRequestHandler<ListarPruebasLabQuery, List<LabTestDto>>
    {
        private readonly HospitalDbContext _context;

        public ListarPruebasLabHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<LabTestDto>> Handle(ListarPruebasLabQuery request, CancellationToken cancellationToken)
        {
            return await _context.Database.SqlQueryRaw<LabTestDto>(@"
                SELECT 
                    T.TestID, 
                    T.TestName, 
                    A.AreaName, 
                    T.Price
                FROM LabTests T
                INNER JOIN LabAreas A ON T.AreaID = A.AreaID
                WHERE T.IsActive = 1
                ORDER BY A.AreaName, T.TestName
            ").ToListAsync(cancellationToken);
        }
    }
}