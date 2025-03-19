using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamPRO.API.Models
{
    public class SubjectCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
