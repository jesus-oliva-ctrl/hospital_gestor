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
        private readonly IUserAccountService _userAccountService;

        public AdminLabService(HospitalDbContext context, IUserEntityFactory userFactory, IUserAccountService userAccountService)
        {
            _context = context;
            _userFactory = userFactory;
            _userAccountService = userAccountService;
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
        public async Task SoftDeleteLabTechnicianAsync(int labTechId)
        {
            var tech = await _context.LaboratoryTechnicians
                .Where(l => l.LabTechId == labTechId)
                .Select(l => new { l.UserId })
                .FirstOrDefaultAsync();

            if (tech == null)
            {
                throw new Exception("Laboratorista no encontrado.");
            }

            await _userAccountService.DeactivateUserEntityAsync(tech.UserId, "Laboratorista");
        }
        public async Task<List<LabTechnicianDto>> GetDeletedTechniciansAsync()
        {
            return await _context.LaboratoryTechnicians
                .IgnoreQueryFilters()
                .Include(l => l.User)
                .Include(l => l.Area)
                .Where(l => !l.IsActive)
                .Select(l => new LabTechnicianDto
                {
                    LabTechID = l.LabTechId,
                    FirstName = l.FirstName,
                    LastName = l.LastName,
                    Phone = l.Phone,
                    Email = l.User.Email,
                    AreaName = l.Area.AreaName
                })
                .ToListAsync();
        }


        public async Task RestoreTechnicianAsync(int labTechId)
        {
            var tech = await _context.LaboratoryTechnicians
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(l => l.LabTechId == labTechId);

            if (tech == null) 
                throw new Exception("Laboratorista no encontrado.");

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_ReactivateEntity @UserID={tech.UserId}, @RoleName='Laboratorista'");
        }
    }
}