using HospitalData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalData.DTOs;
using System.ComponentModel;

namespace HospitalData.Services
{
    public interface IPatientService
    {
        Task<List<MedicalHistoryDto>> GetMyMedicalHistoryAsync(int patientId);
    }
}