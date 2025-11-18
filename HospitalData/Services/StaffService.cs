using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq; // Necesario para el método .Join
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class StaffService : IStaffService
    {
        private readonly HospitalDbContext _context;

        public StaffService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> ObtenerDoctoresAsync()
        {
            return await _context.Doctors
                                 .Include(d => d.Specialty)
                                 .ToListAsync();
        }

        public async Task CrearDoctorAsync(CreateDoctorDto nuevoDoctor)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC SP_CreateNewEntity
                    @FirstName = {nuevoDoctor.FirstName},
                    @LastName = {nuevoDoctor.LastName},
                    @Email = {nuevoDoctor.Email},
                    @Phone = {nuevoDoctor.Phone},
                    @EntityType = 'Medico',
                    @SpecialtyID = {nuevoDoctor.SpecialtyID}
            ");
        }

        // --- MÉTODOS DE GESTIÓN DE PACIENTES ---
        public async Task<List<Patient>> ObtenerPacientesAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task CrearPacienteAsync(CreatePatientDto nuevoPaciente)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC SP_CreateNewEntity
                    @FirstName = {nuevoPaciente.FirstName},
                    @LastName = {nuevoPaciente.LastName},
                    @Email = {nuevoPaciente.Email},
                    @Phone = {nuevoPaciente.Phone},
                    @EntityType = 'Paciente'
            ");
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