using HospitalData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalData.DTOs;
using System.ComponentModel;

namespace HospitalData.Services
{
    public interface IPatientService
    {
        Task<int> GetPatientIdByUserIdAsync(int userId);
        Task<List<MedicalHistoryDto>> GetMyMedicalHistoryAsync(int patientId);
        Task<List<VwPatientAppointment>> GetMyAppointmentsAsync(int patientId);
        Task<List<Doctor>> GetActiveDoctorsAsync();
        Task ScheduleAppointmentAsync(ScheduleAppointmentDto dto);
        Task <List<VwPatientActivePrescription>> GetMyPrescriptionsAsync(int patientId);
        Task CancelAppointmentAsync(int appointmentId);
        Task <PatientProfileDto> GetPatientProfileAsync(int userId);
        Task UpdatePatientProfileAsync(PatientProfileDto dto);
    }
}