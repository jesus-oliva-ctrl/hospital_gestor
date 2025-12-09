using HospitalData.DTOs;
using HospitalData.Models; 

namespace HospitalData.Services
{
    public interface IStaffService
    {
        Task<List<Doctor>> ObtenerDoctoresAsync();
        Task CrearDoctorAsync(CreateDoctorDto nuevoDoctor);

        Task<List<SpecialtyDto>> ObtenerEspecialidadesAsync();
        Task<List<Patient>> ObtenerPacientesAsync();
        Task CrearPacienteAsync(CreatePatientDto nuevoPaciente);
        Task<List<InventoryDto>> ObtenerInventarioAsync();
        Task CrearMedicamentoAsync(CreateMedicationDto nuevoMedicamento);
        Task ActualizarStockAsync(UpdateStockDto stockUpdate);
        Task<List<AppointmentDetailDto>> ObtenerCitasAsync();
    }
}