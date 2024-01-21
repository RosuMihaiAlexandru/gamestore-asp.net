using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Gamesmarket.Service.Interfaces;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Service.Extensions;
using Microsoft.Extensions.Configuration;

namespace Gamesmarket.Service.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user, List<IdentityRole<long>> roles)
        {// Create a JWT token using the claims and configuration settings.
            var token = user
                .CreateClaims(roles)
                .CreateJwtToken(_configuration);
            var tokenHandler = new JwtSecurityTokenHandler();// Use JwtSecurityTokenHandler to write the token as a string.

            return tokenHandler.WriteToken(token);
        }
    }
}
