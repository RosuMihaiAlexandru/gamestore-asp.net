using System.ComponentModel.DataAnnotations;

namespace Gamesmarket.Domain.Enum
{
    public enum GameGenre
    {
        [Display(Name = "RPG")]
        RPG = 0,

        [Display(Name = "Action")]
        Action = 1,

        [Display(Name = "Adventure")]
        Adventure = 2,

        [Display(Name = "Shooter")]
        Shooter = 3,

        [Display(Name = "Simulation")]
        Simulation = 4,

        [Display(Name = "Strategy")]
        Strategy = 5,

        [Display(Name = "Sports")]
        Sports = 6,

        [Display(Name = "Horror")]
        Horror = 7,

        [Display(Name = "Platformer")]
        Platformer = 8,

        [Display(Name = "Fighting")]
        Fighting = 9
    }
}
