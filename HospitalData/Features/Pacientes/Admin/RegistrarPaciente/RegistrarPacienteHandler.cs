using MediatR;
using HospitalData.Models;
using HospitalData.Factories;
using HospitalData.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalData.Features.Pacientes.Admin.RegistrarPaciente
{
    public class RegistrarPacienteHandler : IRequestHandler<RegistrarPacienteCommand, Unit>
    {
        private readonly HospitalDbContext _context;
        private readonly IUserEntityFactory _userFactory;

        public RegistrarPacienteHandler(HospitalDbContext context, IUserEntityFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task<Unit> Handle(RegistrarPacienteCommand request, CancellationToken cancellationToken)
        {
            var parameters = _userFactory.CreateParameters(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                UserType.Paciente,
                null 
            );

            var sql = "EXEC SP_CreateNewEntity @FirstName, @LastName, @Email, @Phone, @EntityType, @SpecialtyID";
            
            await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
            
            return Unit.Value;
        }
    }
}