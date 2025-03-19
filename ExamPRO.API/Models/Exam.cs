using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ExamPRO.API.Models
{
    public class Exam
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; } = null!; // קישור למורה שיצר את המבחן

        [BsonRepresentation(BsonType.ObjectId)]
        public string SubjectId { get; set; } = null!; // קישור למקצוע שאליו שייך המבחן

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
