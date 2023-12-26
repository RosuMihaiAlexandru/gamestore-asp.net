using Gamesmarket.Domain.Enum;

namespace Gamesmarket.Domain.Entity
{
    public class Game
    {//A data model about the games
        public int Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public GameGenre GameGenre { get; set; }
        public string ImagePath { get; set; }

    }

}
