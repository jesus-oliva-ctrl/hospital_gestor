using HospitalData.Models;
using HospitalData.DTOs; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IAdminPatientService
    {
        Task<List<PatientProfileDto>> GetAllPatientsAsync();
        Task CreatePatientAsync(string firstName, string lastName, string email, string phone);
        Task SoftDeletePatientAsync(int patientId);
        Task<List<PatientProfileDto>> GetDeletedPatientsAsync();
        Task RestorePatientAsync(int patientId);
    }
}