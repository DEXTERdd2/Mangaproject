using Microsoft.EntityFrameworkCore;
using MangaBackend.Domain.Entities; 

namespace MangaBackend.Infrastructure.Data
{
    public class MangaBackendDbContext : DbContext
    {
        public MangaBackendDbContext(DbContextOptions<MangaBackendDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tb_User> Tb_Users { get; set; }
        public DbSet<Tb_Comment> Tb_Comments { get; set; }
        public DbSet<Tb_Manga> Tb_Mangas { get; set; }
        public DbSet<Tb_MangaChapter> Tb_MangaChapters { get; set; }
    }
}

