using HospitalData.DTOs;
using HospitalData.Models; 

namespace HospitalData.Services
{
    public interface IStaffService
    {
        Task<List<InventoryDto>> ObtenerInventarioAsync();
        Task CrearMedicamentoAsync(CreateMedicationDto nuevoMedicamento);
        Task ActualizarStockAsync(UpdateStockDto stockUpdate);
        Task<List<AppointmentDetailDto>> ObtenerCitasAsync();
    }
}