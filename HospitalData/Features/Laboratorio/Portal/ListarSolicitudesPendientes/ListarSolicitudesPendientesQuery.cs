using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Laboratorio.Portal.ListarSolicitudesPendientes
{
    public class ListarSolicitudesPendientesQuery : IRequest<List<LabRequestDto>>
    {
        public int UserId { get; set; }
        public ListarSolicitudesPendientesQuery(int userId) => UserId = userId;
    }
}