using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Doctores.Portal.Perfil
{
    public class VerPerfilDoctorHandler : IRequestHandler<VerPerfilDoctorQuery, DoctorProfileDto>
    {
        private readonly HospitalDbContext _context;

        public VerPerfilDoctorHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorProfileDto> Handle(VerPerfilDoctorQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .Include(d => d.User)     
                .Include(d => d.Specialty)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == request.UserId, cancellationToken);

            if (doctor == null) throw new Exception("Perfil no encontrado.");

            return new DoctorProfileDto
            {
                UserID = request.UserId, 
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Phone = doctor.Phone,
                SpecialtyID = doctor.SpecialtyId ?? 0,
                SpecialtyName = doctor.Specialty?.SpecialtyName ?? "Sin Asignar",
                Email = doctor.User?.Email ?? "",
                Username = doctor.User?.Username ?? ""
            };
        }
    }
}