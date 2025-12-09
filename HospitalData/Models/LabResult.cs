using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public class LabResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("RequestID")]
        public int RequestId { get; set; } 

        [BsonElement("PatientID")]
        public int PatientId { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [BsonElement("TestName")]
        public string TestName { get; set; } 

        [BsonElement("LabTechID")]
        public int LabTechId { get; set; } 

        [BsonElement("Results")]
        public Dictionary<string, object> Results { get; set; } = new Dictionary<string, object>();
    }
}