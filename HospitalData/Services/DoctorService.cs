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
        //Dependencias de interfaces
        private readonly HospitalDbContext _context;
        private readonly IAppointmentManagementService _appointmentService;
        private readonly IPrescriptionManagementService _prescriptionService;
        private readonly IUserAccountService _userAccountService;

        //Inyeccion de dependencias a traves del constructor
        public DoctorService(HospitalDbContext context, IAppointmentManagementService appointmentService, IPrescriptionManagementService prescriptionService, IUserAccountService userAccountService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _prescriptionService = prescriptionService;
            _userAccountService = userAccountService;
        }

        //Metodos 
        
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
            await _appointmentService.CancelAppointmentAsync(appointmentId);
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
            return await _prescriptionService.GetPrescriptionsForPatientAsync(patientId);
        }

        public async Task CreatePrescriptionAsync(CreatePrescriptionDto dto)
        {
            await _prescriptionService.CreatePrescriptionAsync(dto);
        }
        public async Task ScheduleNewAppointmentAsync(ScheduleAppointmentDto dto)
        {
            await _appointmentService.ScheduleAppointmentAsync(dto);
        }

        public async Task RescheduleAppointmentAsync(int oldAppointmentId, ScheduleAppointmentDto newAppointmentDto)
        {
            await _appointmentService.CancelAppointmentAsync(oldAppointmentId);
            await _appointmentService.ScheduleAppointmentAsync(newAppointmentDto);
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

        public async Task<DoctorProfileDto> GetDoctorProfileAsync(int userId)
        {
            var doctor = await _context.Doctors
                .Include(d => d.User)     
                .Include(d => d.Specialty)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (doctor == null) throw new Exception("Perfil no encontrado.");

            return new DoctorProfileDto
            {
                UserID = userId, 
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Phone = doctor.Phone,
                SpecialtyID = doctor.SpecialtyId ?? 0,
                SpecialtyName = doctor.Specialty?.SpecialtyName ?? "Sin Asignar",
                Email = doctor.User?.Email ?? "",
                Username = doctor.User?.Username?? ""
            };
        }
        public async Task UpdateDoctorProfileAsync(DoctorProfileDto dto)
        {
            await _userAccountService.UpdateUserProfileAsync(dto);
        }
    }
    
    
}