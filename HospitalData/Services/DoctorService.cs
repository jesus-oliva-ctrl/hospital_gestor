using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.DTOs;

namespace HospitalData.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _context;

        public DoctorService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<VwDoctorAgendaSummary>> GetMyAgendaAsync(int loggedInUserId)
        {

            var doctor = await _context.Doctors
                                    .FirstOrDefaultAsync(d => d.UserId == loggedInUserId);

            if (doctor == null)
            {
                return new List<VwDoctorAgendaSummary>();
            }

            int correctDoctorId = doctor.DoctorId;


            return await _context.VwDoctorAgendaSummaries
                                .Where(cita => cita.DoctorId == correctDoctorId)
                                .OrderBy(cita => cita.AppointmentDate)
                                .ToListAsync();
        }
        public async Task<Appointment?> GetAppointmentDetailsAsync(int appointmentId)
        {
            return await _context.Appointments
                                .Include(a => a.Patient)
                                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task CompleteAppointmentAsync(int appointmentId, string diagnosisNotes)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Cita no encontrada.");
            }

            var historyRecord = new MedicalHistory
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Description = diagnosisNotes,
                VisitDate = DateTime.Now
            };
            _context.MedicalHistories.Add(historyRecord);

            appointment.Status = "Completada";

            await _context.SaveChangesAsync();
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_CancelAppointment @AppointmentID={appointmentId}");
        }

        public async Task<List<MedicalHistoryDto>> GetMyMedicalHistoryAsync(int loggedInUserId)

        {
            var doctor = await _context.Doctors
                           .FirstOrDefaultAsync(d => d.UserId == loggedInUserId);

            if (doctor == null)
            {
                return new List<MedicalHistoryDto>();
            }
            return await _context.MedicalHistories
                .Where(mh => mh.DoctorId == doctor.DoctorId)
                .OrderByDescending(mh => mh.VisitDate)
                .Select(mh => new MedicalHistoryDto
                {
                    HistoryID = mh.HistoryId,
                    VisitDate = mh.VisitDate,
                    Description = mh.Description,
                    PatientFirstName = mh.Patient.FirstName,
                    PatientLastName = mh.Patient.LastName,
                    PatientID = mh.PatientId
                })
                .ToListAsync();
        }
        public async Task<List<InventoryDto>> GetAvailableMedicationsAsync()
        {
            return await _context.Medications
                .Join(_context.Inventories,
                      med => med.MedicationId,
                      inv => inv.MedicationId,
                      (med, inv) => new InventoryDto
                      {
                          MedicationId = med.MedicationId,
                          Name = med.Name,
                          Description = med.Description,
                          Quantity = inv.Quantity,
                          LastUpdated = inv.LastUpdated ?? DateTime.MinValue
                      })
                .Where(inv => inv.Quantity > 0)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<List<Prescription>> GetPrescriptionsForPatientAsync(int patientId)
        {
            return await _context.Prescriptions
                                 .Include(p => p.Medication)
                                 .Where(p => p.PatientId == patientId)
                                 .OrderByDescending(p => p.StartDate)
                                 .ToListAsync();
        }

        public async Task CreatePrescriptionAsync(CreatePrescriptionDto dto)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC SP_IssueNewPrescription
                    @PatientID = {dto.PatientID},
                    @DoctorID = {dto.DoctorID},
                    @MedicationID = {dto.MedicationID},
                    @Dosage = {dto.Dosage},
                    @QuantityToDispense = {dto.QuantityToDispense},
                    @EndDate = {dto.EndDate}
            ");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 50001)
            {
                throw new Exception("Error: No hay suficiente stock del medicamento seleccionado.");
            }
        }

        public async Task ScheduleNewAppointmentAsync(ScheduleAppointmentDto dto)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_ScheduleAppointment
                        @PatientID = {dto.PatientID},
                        @DoctorID = {dto.DoctorID},
                        @AppointmentDate = {dto.AppointmentDate}
                ");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                throw new Exception($"Error al agendar: {ex.Message}");
            }
        }

        public async Task RescheduleAppointmentAsync(int oldAppointmentId, ScheduleAppointmentDto newAppointmentDto)
        {
            await CancelAppointmentAsync(oldAppointmentId);
            await ScheduleNewAppointmentAsync(newAppointmentDto);
        }

        public async Task<int> GetMyDoctorIdAsync(int loggedInUserId)
        {
            var doctor = await _context.Doctors
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(d => d.UserId == loggedInUserId);

            if (doctor == null)
            {
                throw new Exception("Doctor no encontrado.");
            }

            return doctor.DoctorId;
        }
    }
    
    
}