using System.Threading.Tasks; 

namespace HospitalData.Services
{
    public interface ICurrentUserService
    {
        Task<int?> GetCurrentUserIdAsync();
        Task<string> GetCurrentUserNameAsync();
    }
}