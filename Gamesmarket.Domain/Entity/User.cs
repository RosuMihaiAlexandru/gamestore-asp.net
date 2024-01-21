using Microsoft.AspNetCore.Identity;

namespace Gamesmarket.Domain.Entity
{
    public class User : IdentityUser<long>
    {//An Identity data model for user authorization with jwt token
        public string Name { get; set; } = null!;

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
