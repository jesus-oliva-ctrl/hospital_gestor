using HospitalData.Models;

namespace HospitalData.Builders
{
    public interface ILabResultBuilder
    {
        void Reset();
        ILabResultBuilder SetBasicInfo(int requestId, int patientId, int doctorId, int techId, string testName);
        ILabResultBuilder AddParameter(string key, string value);
        ILabResultBuilder AddAttachment(string fileName, string url);
        ILabResultBuilder AddObservations(string notes);
        LabResult Build();
    }
}