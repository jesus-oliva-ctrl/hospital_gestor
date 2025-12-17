using MediatR;
using HospitalData.DTOs; 
using System.Collections.Generic;

namespace HospitalWeb.Features.Doctores.Admin.ListarEspecialidades
{
    public class ListarEspecialidadesQuery : IRequest<List<SpecialtyDto>>
    {
    }
}