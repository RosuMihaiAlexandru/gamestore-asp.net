using Microsoft.AspNetCore.Identity;
using Gamesmarket.Domain.Entity;

namespace Gamesmarket.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken( User user, List<IdentityRole<long>> role);
    }
}
