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
    }
}

