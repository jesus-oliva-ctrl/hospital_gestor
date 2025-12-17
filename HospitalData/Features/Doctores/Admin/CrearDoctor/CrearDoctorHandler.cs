using MediatR;
using HospitalData.Models;
using HospitalData.Factories; 
using HospitalData.Enums;    
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalWeb.Features.Doctores.Admin.CrearDoctor
{
    public class CrearDoctorHandler : IRequestHandler<CrearDoctorCommand, Unit>
    {
        private readonly HospitalDbContext _context;
        private readonly IUserEntityFactory _userFactory;

        public CrearDoctorHandler(HospitalDbContext context, IUserEntityFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task<Unit> Handle(CrearDoctorCommand request, CancellationToken cancellationToken)
        {
            var parameters = _userFactory.CreateParameters(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                UserType.Medico,
                request.SpecialtyID
            );

            var sql = "EXEC SP_CreateNewEntity @FirstName, @LastName, @Email, @Phone, @EntityType, @SpecialtyID";
            
            await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

            return Unit.Value; 
        }
    }
}