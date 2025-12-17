using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Laboratorio.Admin.ListarAreas
{
    public class ListarAreasQuery : IRequest<List<LabAreaDto>> { }
}