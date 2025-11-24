using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly HospitalDbContext _context;

        public UserAccountService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task UpdateUserProfileAsync(UserProfileBaseDto dto, string? address = null)
        {
            
            try 
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_UpdateUserProfile
                        @UserID = {dto.UserID},
                        @Username = {dto.Username},
                        @Email = {dto.Email},
                        @NewPassword = {dto.NewPassword},
                        @FirstName = {dto.FirstName},
                        @LastName = {dto.LastName},
                        @Phone = {dto.Phone},
                        @Address = {address}
                ");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) 
                {
                    throw new Exception("El nombre de usuario ya está en uso.");
                }
                throw;
            }
        }
    }
}