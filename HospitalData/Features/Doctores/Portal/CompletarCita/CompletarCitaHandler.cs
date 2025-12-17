using MediatR;
using HospitalData.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.CompletarCita
{
    public class CompletarCitaHandler : IRequestHandler<CompletarCitaCommand, Unit>
    {
        private readonly HospitalDbContext _context;

        public CompletarCitaHandler(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CompletarCitaCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FindAsync(new object[] { request.AppointmentId }, cancellationToken);
            
            if (appointment == null)
            {
                throw new Exception("Cita no encontrada.");
            }

            var historyRecord = new MedicalHistory
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Description = request.DiagnosisNotes,
                VisitDate = DateTime.Now
            };
            _context.MedicalHistories.Add(historyRecord);

            appointment.Status = "Completada";

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}