using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using HospitalData.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HospitalWeb.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly UserSessionService _sessionService;

        public CustomAuthenticationStateProvider(UserSessionService sessionService)
        {
            _sessionService = sessionService;
            _sessionService.OnChange += StateChanged;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            if (_sessionService.CurrentUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, _sessionService.CurrentUser.UserID.ToString()),
                    new Claim("UserID", _sessionService.CurrentUser.UserID.ToString()), 
                    
                    new Claim(ClaimTypes.Role, _sessionService.CurrentUser.RoleName),
                    
                    new Claim(ClaimTypes.Name, _sessionService.CurrentUser.RoleName) 
                };

                identity = new ClaimsIdentity(claims, "CustomAuth");
            }

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }

        private void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Dispose()
        {
            _sessionService.OnChange -= StateChanged;
        }
    }
}