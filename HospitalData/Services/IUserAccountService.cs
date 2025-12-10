using HospitalData.DTOs;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IUserAccountService
    {    
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task UpdateUserProfileAsync(UserProfileDto profile);
    }
}