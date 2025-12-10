using HospitalData.Models;
using HospitalData.Factories;
using HospitalData.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class AdminPatientService : IAdminPatientService
    {
        private readonly HospitalDbContext _context;
        private readonly IUserEntityFactory _userFactory;

        public AdminPatientService(HospitalDbContext context, IUserEntityFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .OrderBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task CreatePatientAsync(string firstName, string lastName, string email, string phone)
        {
            var parameters = _userFactory.CreateParameters(
                firstName,
                lastName,
                email,
                phone,
                UserType.Paciente, 
                null 
            );

            var sql = "EXEC SP_CreateNewEntity @FirstName, @LastName, @Email, @Phone, @EntityType, @SpecialtyID";
            
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}