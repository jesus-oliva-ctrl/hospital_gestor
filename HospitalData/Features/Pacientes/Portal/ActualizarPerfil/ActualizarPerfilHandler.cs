using MediatR;
using HospitalData.Services; 
using HospitalData.DTOs;     
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Portal.ActualizarPerfil
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
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Address = request.Address,
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