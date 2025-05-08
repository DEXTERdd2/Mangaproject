using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Application.DTOS.MangaDtos
{
    public class MangaDto
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Thumbnail { get; set; }
        public string? Status { get; set; }
        public string[]? Genres { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public double Rating { get; set; }
        public List<MangaChapterDto> Chapters { get; set; } = new List<MangaChapterDto>();
    }
}
