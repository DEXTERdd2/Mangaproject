using MangaBackend_Infrastructure.MongoConfrguration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MangaBackend.Infrastructure.Data
{
    public class MongoConnectionTest
    {
        public readonly IMongoDatabase Database;

        public MongoConnectionTest(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public bool TestConnection()
        {
            try
            {
                var collections = Database.ListCollections().ToList(); // Just fetch collection names
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
