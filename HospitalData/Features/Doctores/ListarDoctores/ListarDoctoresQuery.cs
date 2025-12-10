using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalWeb.Features.Doctores.ListarDoctores
{
    public class ListarDoctoresQuery : IRequest<List<Doctor>>
    {
    }
}