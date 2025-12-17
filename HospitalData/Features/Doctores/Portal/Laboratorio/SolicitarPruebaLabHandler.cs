using MediatR;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.Laboratorio
{
    public class SolicitarPruebaLabHandler : IRequestHandler<SolicitarPruebaLabCommand, Unit>
    {
        private readonly HospitalDbContext _context;

        public SolicitarPruebaLabHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SolicitarPruebaLabCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC SP_CreateLabRequest @AppointmentID={request.AppointmentId}, @TestID={request.TestId}", 
                    cancellationToken);
                
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al solicitar laboratorio: {ex.Message}");
            }
        }
    }
}