using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Pacientes.Portal.Doctores
{
    public class ListarDoctoresActivosQuery : IRequest<List<Doctor>> { }
}