using VideoGame.Abstractions;

namespace VideoGame.Entities
{
    public class Game: BaseEntity
    {
        public string Name { get; set; }
        public string StudioDeveloper { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
