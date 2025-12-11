using HospitalData.DTOs;
using HospitalData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IAdminDoctorService
    {
        Task<List<DoctorProfileDto>> GetAllDoctorsAsync();
        Task<List<SpecialtyDto>> GetSpecialtiesAsync();
        Task CreateDoctorAsync(CreateDoctorDto dto);
        Task SoftDeleteDoctorAsync(int doctorId);
        Task<List<DoctorProfileDto>> GetDeletedDoctorsAsync();
        Task RestoreDoctorAsync(int doctorId);
    }
}