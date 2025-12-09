using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Services
{
    public class AdminLabService : IAdminLabService
    {
        private readonly HospitalDbContext _context;

        public AdminLabService(HospitalDbContext context)
        {
            _context = context;
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
            var sql = "EXEC SP_CreateNewEntity @FirstName={0}, @LastName={1}, @Email={2}, @Phone={3}, @EntityType='Laboratorista', @SpecialtyID={4}";
            await _context.Database.ExecuteSqlRawAsync(sql, name, lastName, email, phone, areaId);
        }

        public async Task UpdateTechnicianAsync(int userId, string username, string email, string name, string lastName, string phone)
        {
            var sql = "EXEC SP_UpdateUserProfile @UserID={0}, @Username={1}, @Email={2}, @FirstName={3}, @LastName={4}, @Phone={5}";
            await _context.Database.ExecuteSqlRawAsync(sql, userId, username, email, name, lastName, phone);
        }
    }
}