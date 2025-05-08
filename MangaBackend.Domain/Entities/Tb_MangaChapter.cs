using MongoDB.Bson.Serialization.Attributes;

namespace MangaBackend.Domain.Entities
{
    public class Tb_MangaChapter
    {
        [BsonElement("id")]
        public string? Id { get; set; }

        public string? Slug { get; set; }
        public string? Title { get; set; }
        public int Number { get; set; }
    }
}
