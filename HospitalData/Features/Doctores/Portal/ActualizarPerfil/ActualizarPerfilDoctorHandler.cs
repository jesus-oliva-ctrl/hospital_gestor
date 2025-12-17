using MediatR;
using HospitalData.Services;
using HospitalData.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Doctores.Portal.Perfil
{
    public class ActualizarPerfilDoctorHandler : IRequestHandler<ActualizarPerfilDoctorCommand, Unit>
    {
        private readonly IUserAccountService _userAccountService;

        public ActualizarPerfilDoctorHandler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task<Unit> Handle(ActualizarPerfilDoctorCommand request, CancellationToken cancellationToken)
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

            if(string.IsNullOrEmpty(request.NewPassword))
            {
                userProfile.NewPassword = null;
                userProfile.ConfirmPassword = null;
            }

            await _userAccountService.UpdateUserProfileAsync(userProfile);
            
            return Unit.Value;
        }
    }
}