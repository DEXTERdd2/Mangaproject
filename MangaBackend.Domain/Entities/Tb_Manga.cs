using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MangaBackend.Domain.Entities
{
    public class Tb_Manga
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Thumbnail { get; set; }
        public string? Status { get; set; }
        public string[]? Genres { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public double Rating { get; set; }

        public List<Tb_MangaChapter> Chapters { get; set; } = new List<Tb_MangaChapter>();
    }
}
