using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Laboratorio.Portal.ObtenerId
{
    public class ObtenerIdHandler : IRequestHandler<ObtenerIdQuery, int>
    {
        private readonly HospitalDbContext _context;

        public ObtenerIdHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ObtenerIdQuery request, CancellationToken cancellationToken)
        {
            var tech = await _context.LaboratoryTechnicians
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(t => t.UserId == request.UserId, cancellationToken);
            
            if (tech == null) throw new Exception("Usuario no es un técnico de laboratorio.");
            
            return tech.LabTechId;
        }
    }
}