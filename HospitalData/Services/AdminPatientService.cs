using HospitalData.Models;
using HospitalData.Factories;
using HospitalData.Enums;
using HospitalData.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class AdminPatientService : IAdminPatientService
    {
        private readonly HospitalDbContext _context;
        private readonly IUserEntityFactory _userFactory;
        private readonly IUserAccountService _userAccountService;

        public AdminPatientService(HospitalDbContext context, IUserEntityFactory userFactory, IUserAccountService userAccountService)
        {
            _context = context;
            _userFactory = userFactory;
            _userAccountService = userAccountService;
        }

        public async Task<List<PatientProfileDto>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.User) 
                .OrderBy(p => p.LastName)
                .Select(p => new PatientProfileDto
                {
                    PatientId = p.PatientId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Email = p.User != null ? p.User.Email : "Sin usuario",
                    DOB = p.Dob, 
                    Gender = p.Gender,
                    Address = p.Address
                })
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
        public async Task SoftDeletePatientAsync(int patientId)
        {
            var patient = await _context.Patients
                .Where(p => p.PatientId == patientId)
                .Select(p => new { p.UserId })
                .FirstOrDefaultAsync();

            if (patient == null || patient.UserId == null)
            {
                throw new Exception("Paciente no encontrado o sin cuenta de usuario asociada.");
            }

            await _userAccountService.DeactivateUserEntityAsync(patient.UserId.Value, "Paciente");
        }
        public async Task<List<PatientProfileDto>> GetDeletedPatientsAsync()
        {
            return await _context.Patients
                .IgnoreQueryFilters() 
                .Include(p => p.User)
                .Where(p => !p.IsActive) 
                .Select(p => new PatientProfileDto
                {
                    PatientId = p.PatientId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Email = p.User.Email,
                    Gender = p.Gender,
                    DOB = p.Dob
                })
                .ToListAsync();
        }

        // 2. Restaurar Paciente
        public async Task RestorePatientAsync(int patientId)
        {
            var patient = await _context.Patients
                .IgnoreQueryFilters() 
                .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (patient == null || patient.UserId == null) 
                throw new Exception("Paciente no encontrado en la papelera.");

            // Llama al SP de Reactivación
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_ReactivateEntity @UserID={patient.UserId}, @RoleName='Paciente'");
        }
    }
}