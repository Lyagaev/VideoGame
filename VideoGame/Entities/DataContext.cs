using Microsoft.EntityFrameworkCore;

namespace VideoGame.Entities
{
    public class DataContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
