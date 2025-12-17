using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Doctores.Portal.Laboratorio
{
    public class ListarPruebasLabQuery : IRequest<List<LabTestDto>> { }
}