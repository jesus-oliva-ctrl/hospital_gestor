using HospitalData.DTOs;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IUserAccountService
    {
        Task UpdateUserProfileAsync(UserProfileBaseDto dto, string? address = null);
    }
}