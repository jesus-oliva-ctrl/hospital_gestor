using HospitalData.DTOs;
using HospitalData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IPrescriptionManagementService
    {
        Task CreatePrescriptionAsync(CreatePrescriptionDto dto);
        Task<List<Prescription>> GetPrescriptionsForPatientAsync(int patientId);        
        Task<List<VwPatientActivePrescription>> GetActivePrescriptionsAsync(int patientId);
    }
}