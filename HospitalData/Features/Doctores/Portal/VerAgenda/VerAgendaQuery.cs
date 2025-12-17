using MediatR;
using HospitalData.Models;
using System.Collections.Generic;

namespace HospitalData.Features.Doctores.Portal.VerAgenda
{
    public class VerAgendaQuery : IRequest<List<VwDoctorAgendaSummary>>
    {
        public int UserId { get; set; }
        public VerAgendaQuery(int userId) => UserId = userId;
    }
}