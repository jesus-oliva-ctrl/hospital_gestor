using HospitalData.Models;
using HospitalData.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface ILabResultService
    {
        Task CreateResultAsync(LabResult result);
        Task<LabResult> GetResultByRequestIdAsync(int requestId);
        Task<List<LabResult>> GetHistoryByPatientIdAsync(int patientId);
        Task<List<HospitalData.DTOs.LabRequestDto>> GetPendingRequestsForUserAsync(int userId);
        Task<LabRequestDto?> GetRequestByIdAsync(int requestId);
        Task CompleteRequestAsync(int requestId);
        Task<int> GetLabTechIdByUserIdAsync(int userId);
        Task<List<LabResult>> GetAllRecentResultsAsync();
    }
}