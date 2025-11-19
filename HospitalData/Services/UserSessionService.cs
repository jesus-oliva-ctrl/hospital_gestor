using HospitalData.DTOs;

namespace HospitalData.Services
{
    public class UserSessionService
    {
        public AuthenticatedUser? CurrentUser { get; private set; }

        public event Action? OnChange;

        public void Login(AuthenticatedUser user)
        {
            CurrentUser = user;
            NotifyStateChanged();
        }

        public void Logout()
        {
            CurrentUser = null;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}