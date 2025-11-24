using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _context;
        private readonly IAppointmentManagementService _appointmentService;
        private readonly IPrescriptionManagementService _prescriptionService;   
        private readonly IUserAccountService _userAccountService;

        public PatientService(HospitalDbContext context, IAppointmentManagementService appointmentService, IPrescriptionManagementService prescriptionService, IUserAccountService userAccountService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _prescriptionService = prescriptionService;
            _userAccountService = userAccountService;
        }

        public async Task<List<MedicalHistoryDto>> GetMyMedicalHistoryAsync(int patientId)
        {
            return await _context.MedicalHistories
                .Where(mh => mh.PatientId == patientId)
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

        public async Task<List<VwPatientAppointment>> GetMyAppointmentsAsync(int patientId)
        {
        return await _context.VwPatientAppointments
                         .Where(a => a.PatientId == patientId)
                         .OrderBy(a => a.AppointmentDate)
                         .ToListAsync();
        }

        public async Task<int> GetPatientIdByUserIdAsync(int userId)
        {
            var patient = await _context.Patients
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.UserId == userId);
            
            if (patient == null) throw new Exception("Perfil de paciente no encontrado para este usuario.");
            
            return patient.PatientId;
        }

        public async Task<List<Doctor>> GetActiveDoctorsAsync()
        {
            return await _context.Doctors
                                 .Include(d => d.Specialty) 
                                 .OrderBy(d => d.LastName)
                                 .ToListAsync();
        }

        public async Task ScheduleAppointmentAsync(ScheduleAppointmentDto dto)
        {
            await _appointmentService.ScheduleAppointmentAsync(dto);
        }

        public async Task<List<VwPatientActivePrescription>> GetMyPrescriptionsAsync(int patientId)
        {
            return await _prescriptionService.GetActivePrescriptionsAsync(patientId);
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            await _appointmentService.CancelAppointmentAsync(appointmentId);
        }

        public async Task<PatientProfileDto> GetPatientProfileAsync(int userId)
        {
            var patient = await _context.Patients
                .Include(p => p.User) 
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null) throw new Exception("Perfil no encontrado.");

            return new PatientProfileDto
            {
                UserID = userId,
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
        public async Task UpdatePatientProfileAsync(PatientProfileDto dto)
        {
            await _userAccountService.UpdateUserProfileAsync(dto, dto.Address);
        }
    }
}
