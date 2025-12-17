using Microsoft.AspNetCore.Components.Authorization; 
using System.Security.Claims;
using HospitalData.Services; 

namespace HospitalWeb.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public CurrentUserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<int?> GetCurrentUserIdAsync()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = state.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated) return null;

            var idClaim = user.FindFirst(c => c.Type == "UserID") ?? user.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }

        public async Task<string> GetCurrentUserNameAsync()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            return state.User.Identity?.Name ?? "Sistema";
        }
    }
}