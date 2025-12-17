using MediatR;
using HospitalData.Models;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Pacientes.Portal.VerPerfil
{
    public class VerPerfilHandler : IRequestHandler<VerPerfilQuery, PatientProfileDto>
    {
        private readonly HospitalDbContext _context;

        public VerPerfilHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<PatientProfileDto> Handle(VerPerfilQuery request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);

            if (patient == null) throw new Exception("Perfil no encontrado.");

            return new PatientProfileDto
            {
                UserID = request.UserId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Phone = patient.Phone,
                Address = patient.Address,
                DateOfBirth = patient.Dob != null ? patient.Dob.Value.ToDateTime(TimeOnly.MinValue) : null,
                Gender = patient.Gender,
                Email = patient.User?.Email ?? "",
                Username = patient.User?.Username ?? ""
            };
        }
    }
}