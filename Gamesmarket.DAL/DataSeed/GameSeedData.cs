using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;

namespace Gamesmarket.DAL.DataSeed
{
    public static class GameSeedData
    {
        public static List<Game> GetStarterGames()
        {
            return new List<Game>
            {
                new Game
                {
                    Id = 1,
                    Name = "Factorio",
                    Developer = "Wube Software",
                    Description = "Factorio is a game about building and creating automated factories to produce items of increasing complexity, within an infinite 2D world. Use your imagination to design your factory, combine simple elements into ingenious structures, and finally protect it from the creatures who don't really like you.",
                    Price = 35,
                    ReleaseDate = DateTime.Parse("2020-08-14"),
                    GameGenre = GameGenre.Strategy,
                    ImagePath = "images/game/df85dfe3-7073-4c41-8ae8-6dfa2d6421c8_Factorio.jpg"
                },
                new Game
                {
                    Id = 2,
                    Name = "The Witcher® 3: Wild Hunt",
                    Developer = "CD PROJEKT RED",
                    Description = "You are Geralt of Rivia, mercenary monster slayer. Before you stands a war-torn, monster-infested continent you can explore at will. Your current contract? Tracking down Ciri — the Child of Prophecy, a living weapon that can alter the shape of the world.",
                    Price = 49,
                    ReleaseDate = DateTime.Parse("2015-05-18"),
                    GameGenre = GameGenre.RPG,
                    ImagePath = "images/game/c956b6e3-707c-4e1b-bbd2-71c46c400b58_4285acda-5fa7-4a2a-91e5-3cb28196ec47_Witcher3.jpg"
                },
                new Game
                {
                    Id = 3,
                    Name = "Stronghold: Definitive Edition",
                    Developer = "FireFly Studios",
                    Description = "Greetings sire! Your stronghold awaits you. Build a castle economy, besiege unforgettable villains and return to the 'castle sim' that started it all. Experience this classic RTS with upgraded visuals, modernised gameplay, Steam multiplayer and a new campaign.",
                    Price = 39,
                    ReleaseDate = DateTime.Parse("2023-11-07"),
                    GameGenre = GameGenre.Strategy,
                    ImagePath = "images/game/bf749d6d-3b50-44ff-8eef-4f9915261663_c8d8354c-3569-4f45-adfe-5fe66ef9d45b_stronghold.jpg"
                },
                new Game
                {
                    Id = 4,
                    Name = "asd",
                    Developer = "meme",
                    Description = "123dcefads",
                    Price = 8,
                    ReleaseDate = DateTime.Parse("2024-02-11"),
                    GameGenre = GameGenre.Adventure,
                    ImagePath = "images/game/6590c7d7-0b89-4eac-945b-ea579c05a438_jhgfd.webp"
                },
                new Game
                {
                    Id = 5,
                    Name = "baldur gateswq",
                    Developer = "sadd",
                    Description = "sdadadad",
                    Price = 123,
                    ReleaseDate = DateTime.Parse("2024-02-11"),
                    GameGenre = GameGenre.Strategy,
                    ImagePath = "images/game/dfc95620-4a3e-42b2-9f81-4a10920e57c5_Baldur's Gate 3.jpg"
                },
                new Game
                {
                    Id = 6,
                    Name = "test game",
                    Developer = "fffef",
                    Description = "efasdrev ew f23 r2cs",
                    Price = 22,
                    ReleaseDate = DateTime.Parse("2024-03-22"),
                    GameGenre = GameGenre.Shooter,
                    ImagePath = "images/game/3efa4f73-05e8-411c-8e97-d14e9c4eb7bc_Factorio.jpg"
                },
                new Game
                {
                    Id = 7,
                    Name = "supergame",
                    Developer = "Firefly Studios",
                    Description = "asd",
                    Price = 2,
                    ReleaseDate = DateTime.Parse("2024-04-06"),
                    GameGenre = GameGenre.Strategy,
                    ImagePath = "images/game/3a171d1b-42e4-4ec6-a06e-ed97bd15de80_c8d8354c-3569-4f45-adfe-5fe66ef9d45b_stronghold.jpg"
                },
                new Game
                {
                    Id = 8,
                    Name = "rpg1",
                    Developer = "asd",
                    Description = "qwdqcc",
                    Price = 12,
                    ReleaseDate = DateTime.Parse("2024-08-09"),
                    GameGenre = GameGenre.RPG,
                    ImagePath = "images/game/b243660e-af37-429a-968e-a8f485841cb5_c8d8354c-3569-4f45-adfe-5fe66ef9d45b_stronghold.jpg"
                },
                new Game
                {
                    Id = 9,
                    Name = "Mortal Kombat 1",
                    Developer = "NetherRealm Studios",
                    Description = "Discover a reborn Mortal Kombat™ Universe created by the Fire God Liu Kang. Mortal Kombat™ 1 ushers in a new era of the iconic franchise with a new fighting system, game modes, and fatalities!",
                    Price = 123,
                    ReleaseDate = DateTime.Parse("2023-10-19"),
                    GameGenre = GameGenre.Action,
                    ImagePath = "images/game/1ca3f97b-a9f8-42c5-b223-50c205eeadcf_641778b6-36f2-4a25-b82d-7f7eb8d8e280_Mortal_Kombat_1_(2023)_cover.jpg"
                },
                new Game
                {
                    Id = 10,
                    Name = "Baldur's Gate 3",
                    Developer = "Larian Studios",
                    Description = "Baldur’s Gate III for PC is an action adventure role playing game, the third in the series, and based, like the others in the series upon Dungeons and Dragons, the cult 80s boardgame that has recently seen a resurgence both online and in-person. You can play alone, or multiplayer in the co-op mode, enjoying the well-rendered characters and amazing graphics that have take a lot of excellent inspiration from the fifth (current) edition of the books.",
                    Price = 53,
                    ReleaseDate = DateTime.Parse("2023-03-17"),
                    GameGenre = GameGenre.RPG,
                    ImagePath = "images/game/9f789cb0-44ce-4d8e-9090-8a2c51f62802_baldur-s-gate-3-pc-game-gog-com-cover.jpg"
                },
                new Game
                {
                    Id = 11,
                    Name = "7 Days to Die",
                    Developer = "The Fun Pimps",
                    Description = "7 Days to Die for PC began life as an early access funded horror survival game that succeeded in hitting its target and made its way onto the market as a complete and polished product. In the game, the player must survive not only enemies and combat, but simply living: finding food, water, shelter and so on are all vital to the character’s continued existence.",
                    Price = 17,
                    ReleaseDate = DateTime.Parse("2023-03-17"),
                    GameGenre = GameGenre.Simulation,
                    ImagePath = "images/game/6a920618-554a-4e93-89be-c4575a6cf0cd_7-days-to-die-pc-mac-game-steam-cover.jpg"
                },
                new Game
                {
                    Id = 12,
                    Name = "UBOAT",
                    Developer = "Deep Water Studio",
                    Description = "UBOAT is a simulator of a submarine from WWII era, yet different than all you have seen so far. It is a survival sandbox with crew management mechanics while its primary theme is life of German sailors. The boat is their home, but it can become their grave at any time.In UBOAT you control the crew in order to control the boat. You look after their physical and mental health, because if the sailors are hungry, tired and their spirit is low, there’s no chance of winning even a skirmish.",
                    Price = 8,
                    ReleaseDate = DateTime.Parse("2024-06-22"),
                    GameGenre = GameGenre.Simulation,
                    ImagePath = "images/game/97cb9299-5877-4f07-81a4-5cbbf36dcb55_uboat-pc-game-steam-cover.jpg"
                },
                new Game
                {
                    Id = 13,
                    Name = "dadadadad",
                    Developer = "asd",
                    Description = "asdasd",
                    Price = 12.5M,
                    ReleaseDate = DateTime.Parse("2024-08-28"),
                    GameGenre = GameGenre.Action,
                    ImagePath = "images/game/10f11362-fd0e-4e6d-95e4-10fb5603e0aa_baldur-s-gate-3-pc-game-gog-com-cover.jpg"
                },
            };
        }
    }

}
