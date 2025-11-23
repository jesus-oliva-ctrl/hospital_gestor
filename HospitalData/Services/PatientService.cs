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

        public PatientService(HospitalDbContext context, IAppointmentManagementService appointmentService, IPrescriptionManagementService prescriptionService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _prescriptionService = prescriptionService;
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
    }
}
