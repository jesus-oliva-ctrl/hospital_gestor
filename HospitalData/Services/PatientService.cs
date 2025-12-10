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
        //Dependencias de interfaces
        private readonly HospitalDbContext _context;
        private readonly IAppointmentManagementService _appointmentService;
        private readonly IPrescriptionManagementService _prescriptionService;   
        private readonly IUserAccountService _userAccountService;

        //Inyeccion de dependencias a traves del constructor
        public PatientService(HospitalDbContext context, IAppointmentManagementService appointmentService, IPrescriptionManagementService prescriptionService, IUserAccountService userAccountService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _prescriptionService = prescriptionService;
            _userAccountService = userAccountService;
        }

        //Metodos

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
            var userProfile = new UserProfileDto
                {
                    UserID = dto.UserID,
                    Username = dto.Username,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Phone = dto.Phone,                    
                    Address = dto.Address, 
                    NewPassword = dto.NewPassword,
                    ConfirmPassword = dto.NewPassword
                };
            if (string.IsNullOrEmpty(dto.NewPassword))
            {
                userProfile.NewPassword = null;
                userProfile.ConfirmPassword = null;
            }

            await _userAccountService.UpdateUserProfileAsync(userProfile);        
        }
    }
}
