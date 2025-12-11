using HospitalData.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IAdminLabService
    {
        Task<List<LabTechnicianDto>> GetAllTechniciansAsync();
        Task<List<LabAreaDto>> GetAllAreasAsync();
        Task CreateTechnicianAsync(string name, string lastName, string email, string phone, int areaId);
        Task UpdateTechnicianAsync(int userId, string username, string email, string name, string lastName, string phone);
        Task SoftDeleteLabTechnicianAsync(int labTechId);
        Task<List<LabTechnicianDto>> GetDeletedTechniciansAsync();
        Task RestoreTechnicianAsync(int labTechId);
    }
}