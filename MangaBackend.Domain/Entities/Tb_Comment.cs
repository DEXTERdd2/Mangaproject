
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MangaBackend.Domain.Entities
{
  public  class Tb_Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? MangaSlug { get; set; }
        public string? ChapterSlug { get; set; }
        public string? Username { get; set; }
        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
