using ExamPRO.API.Models;
using ExamPRO.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Services
{
    public class StudyMaterialService
    {
        private readonly IMongoCollection<StudyMaterial> _studyMaterialsCollection;

        public StudyMaterialService(IOptions<MongoDbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _studyMaterialsCollection = database.GetCollection<StudyMaterial>("StudyMaterials");
        }

        public async Task<List<StudyMaterial>> GetAsync() =>
            await _studyMaterialsCollection.Find(_ => true).ToListAsync();

        public async Task<StudyMaterial?> GetByIdAsync(string id) =>
            await _studyMaterialsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(StudyMaterial studyMaterial) =>
            await _studyMaterialsCollection.InsertOneAsync(studyMaterial);

        public async Task UpdateAsync(string id, StudyMaterial studyMaterial) =>
            await _studyMaterialsCollection.ReplaceOneAsync(x => x.Id == id, studyMaterial);

        public async Task DeleteAsync(string id) =>
            await _studyMaterialsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
