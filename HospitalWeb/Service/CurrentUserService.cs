using HospitalData.Services;

namespace HospitalWeb.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly UserSessionService _sessionService;

        public CurrentUserService(UserSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public Task<int?> GetCurrentUserIdAsync()
        {
            return Task.FromResult(_sessionService.CurrentUser?.UserID);
        }

        public Task<string> GetCurrentUserNameAsync()
        {
            var userName = _sessionService.CurrentUser?.RoleName ?? "Sistema";
            return Task.FromResult(userName);
        }
    }
}