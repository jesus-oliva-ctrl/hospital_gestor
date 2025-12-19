using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; 
using System;
using System.Linq;

namespace HospitalData.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly HospitalDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UserAccountService(HospitalDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<UserProfileDto> GetUserProfileAsync(int userId)
        {
            var result = await _context.Database
                .SqlQueryRaw<UserProfileDto>("EXEC SP_GetProfileData @UserID={0}", userId)
                .ToListAsync();

            return result.FirstOrDefault() ?? new UserProfileDto();
        }

        public async Task UpdateUserProfileAsync(UserProfileDto profile)
        {
            var currentUserId = await _currentUserService.GetCurrentUserIdAsync();
            var currentUserName = await _currentUserService.GetCurrentUserNameAsync();
            
            if (currentUserId.HasValue) 
            {
                await _context.SetAuditContextAsync(currentUserId.Value, currentUserName);
            }

            var pUserId = new SqlParameter("@UserID", profile.UserID);
            var pUsername = new SqlParameter("@Username", profile.Username);
            var pEmail = new SqlParameter("@Email", profile.Email);
            var pFirstName = new SqlParameter("@FirstName", profile.FirstName);
            var pLastName = new SqlParameter("@LastName", profile.LastName);
            
            var pPhone = new SqlParameter("@Phone", (object?)profile.Phone ?? DBNull.Value);

            var pPassword = new SqlParameter("@NewPassword", 
                string.IsNullOrEmpty(profile.NewPassword) ? DBNull.Value : profile.NewPassword);

            var pAddress = new SqlParameter("@Address", 
                string.IsNullOrEmpty(profile.Address) ? DBNull.Value : profile.Address);

            var sql = @"EXEC SP_UpdateUserProfile 
                        @UserID, 
                        @Username, 
                        @Email, 
                        @NewPassword, 
                        @FirstName, 
                        @LastName, 
                        @Phone, 
                        @Address";

            await _context.Database.ExecuteSqlRawAsync(sql, 
                pUserId, 
                pUsername, 
                pEmail, 
                pPassword, 
                pFirstName, 
                pLastName, 
                pPhone, 
                pAddress
            );
        }

        public async Task DeactivateUserEntityAsync(int userId, string roleName)
        {
            try
            {
                var currentUserId = await _currentUserService.GetCurrentUserIdAsync();
                var currentUserName = await _currentUserService.GetCurrentUserNameAsync();

                if (currentUserId.HasValue) 
                {
                    await _context.SetAuditContextAsync(currentUserId.Value, currentUserName);
                }

                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC SP_DeactivateEntity @UserID={userId}, @RoleName={roleName}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al desactivar el usuario: {ex.Message}");
            }
        }
    }
}