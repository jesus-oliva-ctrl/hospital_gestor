using HospitalData.DTOs;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public interface IAppointmentManagementService
    {
        Task ScheduleAppointmentAsync(ScheduleAppointmentDto dto);        
        Task CancelAppointmentAsync(int appointmentId);
    }
}