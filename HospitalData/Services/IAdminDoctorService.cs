using HospitalData.DTOs;
using HospitalData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IAdminDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<List<SpecialtyDto>> GetSpecialtiesAsync();
        Task CreateDoctorAsync(CreateDoctorDto dto);
    }
}