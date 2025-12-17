using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HospitalData.Features.Pacientes.Portal.ObtenerId
{
    public class ObtenerIdPacienteHandler : IRequestHandler<ObtenerIdPacienteQuery, int>
    {
        private readonly HospitalDbContext _context;

        public ObtenerIdPacienteHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ObtenerIdPacienteQuery request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);
            
            if (patient == null) throw new Exception("Perfil de paciente no encontrado para este usuario.");
            
            return patient.PatientId;
        }
    }
}