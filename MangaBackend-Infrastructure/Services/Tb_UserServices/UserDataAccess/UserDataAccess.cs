using MangaBackend_Application.DTOS.Tb_UserDtos;
using MangaBackend_Infrastructure.MongoConfrguration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MangaBackend.Infrastructure.Data
{
    public class MongoUserAccess
    {
        private readonly IMongoCollection<UserDto> _users;

        public MongoUserAccess(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<UserDto>("Users");
        }

        public List<UserDto> GetAll() => _users.Find(_ => true).ToList();
        public UserDto? GetById(string id)
        {
            return _users.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Add(UserDto dto) => _users.InsertOne(dto);
        public bool Update(string id, UserDto dto)
        {
            var result = _users.ReplaceOne(x => x.Id == id, dto);
            return result.ModifiedCount > 0;
        }
        public bool Delete(string id)
        {
            var result = _users.DeleteOne(x => x.Id == id);
            return result.DeletedCount > 0;
        }
      
        public bool Exists(string? id, string email, string username)
        {
            return _users.Find(u =>
                (id != null && u.Id == id) ||
                u.Email.ToLower() == email.ToLower() ||
                u.Username.ToLower() == username.ToLower()
            ).Any();
        }
    }
}