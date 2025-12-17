using MediatR;
using HospitalData.DTOs; // Reutilizamos el DTO existente
using System.Collections.Generic;

namespace HospitalData.Features.Laboratorio.Admin.ListarTecnicos
{
    public class ListarTecnicosQuery : IRequest<List<LabTechnicianDto>> { }
}