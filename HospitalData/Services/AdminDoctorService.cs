using HospitalData.DTOs;
using HospitalData.Models;
using HospitalData.Factories; 
using HospitalData.Enums;    
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HospitalData.Services
{
    public class AdminDoctorService : IAdminDoctorService
    {
        private readonly HospitalDbContext _context;
        private readonly IUserEntityFactory _userFactory;

        public AdminDoctorService(HospitalDbContext context, IUserEntityFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Specialty) 
                .OrderBy(d => d.LastName)
                .ToListAsync();
        }

        public async Task<List<SpecialtyDto>> GetSpecialtiesAsync()
        {
            return await _context.Specialties
                .Select(s => new SpecialtyDto 
                { 
                    SpecialtyId = s.SpecialtyId, 
                    SpecialtyName = s.SpecialtyName 
                })
                .ToListAsync();
        }

        public async Task CreateDoctorAsync(CreateDoctorDto dto)
        {

            var parameters = _userFactory.CreateParameters(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Phone,
                UserType.Medico, 
                dto.SpecialtyID  
            );

            var sql = "EXEC SP_CreateNewEntity @FirstName, @LastName, @Email, @Phone, @EntityType, @SpecialtyID";
            
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}