using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ExamPRO.API.Models
{
    public class StudyMaterial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string FileName { get; set; } = null!; // שם הקובץ
        public string FilePath { get; set; } = null!; // נתיב הקובץ (בשרת או בענן)

        [BsonRepresentation(BsonType.ObjectId)]
        public string UploadedByTeacherId { get; set; } = null!; // המורה שהעלה את הקובץ

        [BsonRepresentation(BsonType.ObjectId)]
        public string SubjectId { get; set; } = null!; // המקצוע של הקובץ

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
