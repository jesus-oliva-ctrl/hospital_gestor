using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalWeb.Features.Doctores.Admin.ListarDoctores
{
    public class ListarDoctoresQuery : IRequest<List<Doctor>>
    {
    }
}