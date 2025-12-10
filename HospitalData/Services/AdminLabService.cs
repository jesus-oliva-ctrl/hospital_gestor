using HospitalData.DTOs;
using HospitalData.Factories;
using HospitalData.Models;
using HospitalData.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class AdminLabService : IAdminLabService
    {
        private readonly HospitalDbContext _context;
        private readonly IUserEntityFactory _userFactory;

        public AdminLabService(HospitalDbContext context, IUserEntityFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task<List<LabTechnicianDto>> GetAllTechniciansAsync()
        {
            return await _context.Database
                .SqlQueryRaw<LabTechnicianDto>("EXEC SP_GetAllLabTechnicians")
                .ToListAsync();
        }

        public async Task<List<LabAreaDto>> GetAllAreasAsync()
        {
            return await _context.Database
                .SqlQueryRaw<LabAreaDto>("SELECT AreaID, AreaName FROM LabAreas")
                .ToListAsync();
        }

        public async Task CreateTechnicianAsync(string name, string lastName, string email, string phone, int areaId)
        {
            var parameters = _userFactory.CreateParameters(
                name, 
                lastName, 
                email, 
                phone, 
                UserType.Laboratorista, 
                areaId 
            );
            var sql = "EXEC SP_CreateNewEntity @FirstName, @LastName, @Email, @Phone, @EntityType, @SpecialtyID";
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task UpdateTechnicianAsync(int userId, string username, string email, string name, string lastName, string phone)
        {
            var sql = "EXEC SP_UpdateUserProfile @UserID={0}, @Username={1}, @Email={2}, @FirstName={3}, @LastName={4}, @Phone={5}";
            await _context.Database.ExecuteSqlRawAsync(sql, userId, username, email, name, lastName, phone);
        }
    }
}