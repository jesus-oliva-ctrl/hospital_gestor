using MediatR;

namespace HospitalWeb.Features.Doctores.CrearDoctor
{
    public class CrearDoctorCommand : IRequest<Unit> 
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int SpecialtyID { get; set; }
    }
}