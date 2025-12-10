using HospitalData.Models;
using System;
using System.Collections.Generic;

namespace HospitalData.Builders
{
    public class LabResultBuilder : ILabResultBuilder
    {
        private LabResult _labResult;

        public LabResultBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _labResult = new LabResult
            {
                Date = DateTime.UtcNow,
                Results = new Dictionary<string, object>()
            };
        }

        public ILabResultBuilder SetBasicInfo(int requestId, int patientId, int doctorId, int techId, string testName)
        {
            _labResult.RequestId = requestId;
            _labResult.PatientId = patientId;
            _labResult.DoctorId = doctorId;
            _labResult.LabTechId = techId;
            _labResult.TestName = testName;
            return this;
        }

        public ILabResultBuilder AddParameter(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
            {
                _labResult.Results[key] = value;
            }
            return this;
        }

        public ILabResultBuilder AddAttachment(string fileName, string url)
        {
            string key = $"Archivo: {fileName}";
            _labResult.Results[key] = url;
            return this;
        }

        public ILabResultBuilder AddObservations(string notes)
        {
            if (!string.IsNullOrWhiteSpace(notes))
            {
                _labResult.Results["Observaciones"] = notes;
            }
            return this;
        }

        public LabResult Build()
        {
            if (_labResult.RequestId == 0) 
                throw new InvalidOperationException("No se ha asignado un RequestID");

            return _labResult;
        }
    }
}