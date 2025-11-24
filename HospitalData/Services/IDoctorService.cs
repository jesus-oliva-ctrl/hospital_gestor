using HospitalData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalData.DTOs;
using System.ComponentModel;

namespace HospitalData.Services
{
    public interface IDoctorService
    {
        Task<List<VwDoctorAgendaSummary>> GetMyAgendaAsync(int doctorId);
        Task<Appointment?> GetAppointmentDetailsAsync(int appointmentId);
        Task CompleteAppointmentAsync(int appointmentId, string diagnosisNotes);
        Task CancelAppointmentAsync(int appointmentId);
        Task<List<MedicalHistoryDto>> GetMyMedicalHistoryAsync(int doctorId);
        Task<List<InventoryDto>> GetAvailableMedicationsAsync();
        Task<List<Prescription>> GetPrescriptionsForPatientAsync(int patientId);
        Task CreatePrescriptionAsync(CreatePrescriptionDto dto);

        Task ScheduleNewAppointmentAsync(ScheduleAppointmentDto dto);
        Task RescheduleAppointmentAsync(int oldAppointmentId, ScheduleAppointmentDto newAppointmentDto);
        Task <int> GetMyDoctorIdAsync(int loggedInUserId);
        Task<DoctorProfileDto> GetDoctorProfileAsync(int userId);
        Task UpdateDoctorProfileAsync(DoctorProfileDto dto);

    }
}