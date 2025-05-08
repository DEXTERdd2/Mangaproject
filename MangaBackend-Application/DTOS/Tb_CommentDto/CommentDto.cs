

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MangaBackend_Application.DTOS.Tb_CommentDto
{
    public class CommentDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? MangaSlug { get; set; }
        public string? ChapterSlug { get; set; }
        public string? Username { get; set; }
        public string? Content { get; set; }
        public object? CreatedAt { get; set; }
    }
}
