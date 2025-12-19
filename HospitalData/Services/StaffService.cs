using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Factories;

namespace HospitalData.Services
{
    public class StaffService : IStaffService
    {
        private readonly HospitalDbContext _context;
        
        private readonly ICurrentUserService _currentUserService;

        public StaffService(HospitalDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<InventoryDto>> ObtenerInventarioAsync()
        {
            return await _context.Medications
                .Join(_context.Inventories,
                      med => med.MedicationId,
                      inv => inv.MedicationId,
                        (med, inv) => new InventoryDto
                      {
                          MedicationId = med.MedicationId,
                          Name = med.Name,
                          Description = med.Description,
                          Quantity = inv.Quantity,
                          LastUpdated = inv.LastUpdated ?? DateTime.MinValue
                      })
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task CrearMedicamentoAsync(CreateMedicationDto nuevoMedicamento)
        {
            
            var userId = await _currentUserService.GetCurrentUserIdAsync();
            var userName = await _currentUserService.GetCurrentUserNameAsync();

            if (userId.HasValue) 
            {
                await _context.SetAuditContextAsync(userId.Value, userName);
            }

            var medication = new Medication
            {
                Name = nuevoMedicamento.Name,
                Description = nuevoMedicamento.Description
            };
            var inventoryItem = new Inventory
            {
                Medication = medication,
                Quantity = nuevoMedicamento.InitialQuantity,
                LastUpdated = DateTime.Now
            };

            _context.Medications.Add(medication);
            _context.Inventories.Add(inventoryItem);

            await _context.SaveChangesAsync();
        }

        public async Task ActualizarStockAsync(UpdateStockDto stockUpdate)
        {
            // Auditoría
            var userId = await _currentUserService.GetCurrentUserIdAsync();
            var userName = await _currentUserService.GetCurrentUserNameAsync();

            if (userId.HasValue) 
            {
                await _context.SetAuditContextAsync(userId.Value, userName);
            }

            var inventoryItem = await _context.Inventories
                .FirstOrDefaultAsync(i => i.MedicationId == stockUpdate.MedicationId);

            if (inventoryItem != null)
            {
                inventoryItem.Quantity += stockUpdate.QuantityToAdd;
                inventoryItem.LastUpdated = DateTime.Now;
                
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El medicamento no fue encontrado en el inventario.");
            }
        }

        public async Task<List<AppointmentDetailDto>> ObtenerCitasAsync()
        {
            return await _context.StaffAppointmentManagementView
                                .OrderByDescending(a => a.AppointmentDate)
                                .ToListAsync();
        }
    }
}