using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Pacientes.Admin.ListarPacientes
{
    public class ListarPacientesQuery : IRequest<List<Patient>> { }
}