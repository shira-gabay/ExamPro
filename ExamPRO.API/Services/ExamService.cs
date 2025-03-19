using ExamPRO.API.Models;
using ExamPRO.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Services
{
    public class ExamService
    {
        private readonly IMongoCollection<Exam> _examsCollection;

        public ExamService(IOptions<MongoDbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _examsCollection = database.GetCollection<Exam>("Exams");
        }

        public async Task<List<Exam>> GetAsync() =>
            await _examsCollection.Find(_ => true).ToListAsync();

        public async Task<Exam?> GetByIdAsync(string id) =>
            await _examsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Exam exam) =>
            await _examsCollection.InsertOneAsync(exam);

        public async Task UpdateAsync(string id, Exam exam) =>
            await _examsCollection.ReplaceOneAsync(x => x.Id == id, exam);

        public async Task DeleteAsync(string id) =>
            await _examsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
