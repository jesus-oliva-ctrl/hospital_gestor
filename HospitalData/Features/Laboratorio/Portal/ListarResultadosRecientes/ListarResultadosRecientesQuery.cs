using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Laboratorio.Portal.ListarResultadosRecientes
{
    public class ListarResultadosRecientesQuery : IRequest<List<LabResult>> { }
}