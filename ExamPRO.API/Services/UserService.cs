using ExamPRO.API.Models;
using ExamPRO.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<MongoDbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetByIdAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _usersCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User user) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, user);

        public async Task DeleteAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
