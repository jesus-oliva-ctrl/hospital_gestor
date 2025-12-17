using MediatR;
using HospitalData.DTOs;
using System.Collections.Generic;

namespace HospitalData.Features.Staff.VerTableroCitas
{
    public class VerTableroCitasQuery : IRequest<List<AppointmentDetailDto>> { }
}