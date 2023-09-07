using VideoGame.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VideoGame.Entities
{
    public class Genre: BaseEntity
    {
        public string Name { get; set; }

        public int? GameId { get; set; }
    }
}
