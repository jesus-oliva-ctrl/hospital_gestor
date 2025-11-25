using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    //Clase para la gestion de citas medicas
    public class AppointmentManagementService : IAppointmentManagementService
    {
        private readonly HospitalDbContext _context;

        public AppointmentManagementService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task ScheduleAppointmentAsync(ScheduleAppointmentDto dto)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_ScheduleAppointment
                        @PatientID = {dto.PatientID},
                        @DoctorID = {dto.DoctorID},
                        @AppointmentDate = {dto.AppointmentDate}
                ");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                throw new Exception($"Error al agendar la cita: {ex.Message}");
            }
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
             await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_CancelAppointment @AppointmentID={appointmentId}");
        }
    }
}