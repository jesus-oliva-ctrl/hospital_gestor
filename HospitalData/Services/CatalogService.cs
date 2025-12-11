using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using HospitalData.DTOs;
using System;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HospitalDbContext _context;

        public CatalogService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task DeactivateMedicationAsync(int medicationId)
        {
            await DeactivateItemAsync(medicationId, "Medicamento");
        }

        public async Task DeactivateLabTestAsync(int testId)
        {
            await DeactivateItemAsync(testId, "Examen");
        }

        public async Task DeactivateLabAreaAsync(int areaId)
        {
            await DeactivateItemAsync(areaId, "Area");
        }

        public async Task DeactivateSpecialtyAsync(int specialtyId)
        {
            await DeactivateItemAsync(specialtyId, "Especialidad");
        }

        private async Task DeactivateItemAsync(int itemId, string itemType)
        {
            try
            {
                if (itemId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0.");

                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC SP_DeactivateCatalogItem @ItemID={itemId}, @ItemType={itemType}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al desactivar {itemType}: {ex.Message}");
            }
        }
        public async Task ModifyMedicationAsync(ModifyMedicationDto dto)
        {
            try
            {
                if (dto.MedicationId <= 0)
                    throw new ArgumentException("ID de medicamento no válido.");


                if (dto.QuantityAdjustment == 0) dto.QuantityAdjustment = null;

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_ModifyMedication 
                        @MedicationID = {dto.MedicationId}, 
                        @Name = {dto.Name}, 
                        @Description = {dto.Description}, 
                        @QuantityAdjustment = {dto.QuantityAdjustment}
                ");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 51002) 
                    throw new Exception("Operación rechazada: El ajuste dejaría el stock en negativo.");
                
                if (ex.Number == 51000)
                    throw new Exception("El medicamento no existe.");

                throw new Exception($"Error de base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar medicamento: {ex.Message}");
            }
        }
        public async Task<List<InventoryDto>> GetDeletedMedicationsAsync()
        {
            
            return await _context.Inventories
                .IgnoreQueryFilters() 
                .Include(i => i.Medication)
                .Where(i => !i.Medication.IsActive) 
                .Select(i => new InventoryDto
                {
                    MedicationId = i.MedicationId,
                    Name = i.Medication.Name,
                    Description = i.Medication.Description,
                    Quantity = i.Quantity,
                    LastUpdated = i.LastUpdated ?? DateTime.MinValue
                })
                .ToListAsync();
        }

        public async Task RestoreMedicationAsync(int medicationId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_ReactivateCatalogItem @ItemID={medicationId}, @ItemType='Medicamento'");
        }
    }
}