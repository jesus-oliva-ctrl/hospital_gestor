using System.Threading.Tasks;
using HospitalData.DTOs;

namespace HospitalData.Services
{
    public interface ICatalogService
    {
        Task DeactivateMedicationAsync(int medicationId);
        Task DeactivateLabTestAsync(int testId);
        Task DeactivateLabAreaAsync(int areaId);
        Task DeactivateSpecialtyAsync(int specialtyId);
        Task ModifyMedicationAsync(ModifyMedicationDto dto);
        Task<List<InventoryDto>> GetDeletedMedicationsAsync();
        Task RestoreMedicationAsync(int medicationId);
    }
}