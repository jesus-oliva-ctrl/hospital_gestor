using MediatR;
using HospitalData.Services; 
using HospitalData.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Laboratorio.Portal.ActualizarPerfil
{
    public class ActualizarPerfilHandler : IRequestHandler<ActualizarPerfilCommand, Unit>
    {
        private readonly IUserAccountService _userAccountService;

        public ActualizarPerfilHandler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task<Unit> Handle(ActualizarPerfilCommand request, CancellationToken cancellationToken)
        {
            var userProfile = new UserProfileDto
            {
                UserID = request.UserID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Username = request.Username,
                Address = null, 
                NewPassword = request.NewPassword,
                ConfirmPassword = request.NewPassword
            };

            if (string.IsNullOrEmpty(request.NewPassword))
            {
                userProfile.NewPassword = null;
                userProfile.ConfirmPassword = null;
            }

            await _userAccountService.UpdateUserProfileAsync(userProfile);

            return Unit.Value;
        }
    }
}