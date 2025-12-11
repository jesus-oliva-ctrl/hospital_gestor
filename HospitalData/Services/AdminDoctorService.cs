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
        private readonly IUserAccountService _userAccountService;

        public AdminDoctorService(HospitalDbContext context, IUserEntityFactory userFactory, IUserAccountService userAccountService)
        {
            _context = context;
            _userFactory = userFactory;
            _userAccountService = userAccountService;
        }

        public async Task<List<DoctorProfileDto>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .Include(d => d.User) 
                .OrderBy(d => d.LastName)
                .Select(d => new DoctorProfileDto
                {
                    DoctorId = d.DoctorId,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Phone = d.Phone,
                    Email = d.User != null ? d.User.Email : "Sin usuario",
                    SpecialtyName = d.Specialty != null ? d.Specialty.SpecialtyName : "Sin Especialidad",
                    SpecialtyID = d.SpecialtyId ?? 0
                })
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
        public async Task SoftDeleteDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors
                .Where(d => d.DoctorId == doctorId)
                .Select(d => new { d.UserId })
                .FirstOrDefaultAsync();

            if (doctor == null || doctor.UserId == null)
                throw new Exception("Doctor no encontrado.");

            await _userAccountService.DeactivateUserEntityAsync(doctor.UserId.Value, "Medico");
        }
        public async Task<List<DoctorProfileDto>> GetDeletedDoctorsAsync()
        {
            return await _context.Doctors
                .IgnoreQueryFilters()
                .Include(d => d.User)
                .Include(d => d.Specialty)
                .Where(d => !d.IsActive)
                .Select(d => new DoctorProfileDto
                {
                    DoctorId = d.DoctorId,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Phone = d.Phone,
                    Email = d.User.Email,
                    SpecialtyName = d.Specialty.SpecialtyName
                })
                .ToListAsync();
        }

        public async Task RestoreDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);

            if (doctor == null || doctor.UserId == null) 
                throw new Exception("Doctor no encontrado.");

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_ReactivateEntity @UserID={doctor.UserId}, @RoleName='Medico'");
        }
    }
}