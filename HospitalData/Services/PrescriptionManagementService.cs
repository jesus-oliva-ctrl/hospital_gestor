using HospitalData.DTOs;
using HospitalData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalData.Services
{
    public class PrescriptionManagementService : IPrescriptionManagementService
    {
        private readonly HospitalDbContext _context;

        public PrescriptionManagementService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task CreatePrescriptionAsync(CreatePrescriptionDto dto)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_IssueNewPrescription
                        @PatientID = {dto.PatientID},
                        @DoctorID = {dto.DoctorID},
                        @MedicationID = {dto.MedicationID},
                        @Dosage = {dto.Dosage},
                        @QuantityToDispense = {dto.QuantityToDispense},
                        @EndDate = {dto.EndDate}
                ");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 50001)
            {
                throw new Exception("Error: No hay suficiente stock del medicamento seleccionado.");
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsForPatientAsync(int patientId)
        {
            return await _context.Prescriptions
                                 .Include(p => p.Medication)
                                 .Include(p => p.Doctor) 
                                 .Where(p => p.PatientId == patientId)
                                 .OrderByDescending(p => p.StartDate)
                                 .ToListAsync();
        }

        public async Task<List<VwPatientActivePrescription>> GetActivePrescriptionsAsync(int patientId)
        {
            return await _context.VwPatientActivePrescriptions
                                 .Where(p => p.PatientId == patientId)
                                 .OrderBy(p => p.EndDate)
                                 .ToListAsync();
        }
    }
}