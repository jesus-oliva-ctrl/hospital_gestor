using HospitalData.Models;
using HospitalData.DTOs; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IAdminPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task CreatePatientAsync(string firstName, string lastName, string email, string phone);
    }
}