using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Application.DTOS.MangaDtos
{
    public class MangaChapterDto
    {
        public string? Id { get; set; }
        public string? Slug { get; set; }
        public string? Title { get; set; }
        public int Number { get; set; }
    }
}
