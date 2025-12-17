using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Laboratorio.Portal.VerPerfil
{
    public class VerPerfilHandler : IRequestHandler<VerPerfilQuery, UserProfileDto>
    {
        private readonly HospitalDbContext _context;

        public VerPerfilHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDto> Handle(VerPerfilQuery request, CancellationToken cancellationToken)
        {
            var tech = await _context.LaboratoryTechnicians
                .Include(t => t.User)
                .Include(t => t.Area)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.UserId == request.UserId, cancellationToken);

            if (tech == null) throw new Exception("Perfil de laboratorista no encontrado.");

            return new UserProfileDto
            {
                UserID = request.UserId,
                FirstName = tech.FirstName,
                LastName = tech.LastName,
                Email = tech.User?.Email ?? "",
                Username = tech.User?.Username ?? "",
                Phone = tech.Phone,
                Address = tech.Area?.AreaName ?? "Sin Área Asignada" 
            };
        }
    }
}