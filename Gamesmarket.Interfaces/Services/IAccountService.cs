using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Identity;

namespace Gamesmarket.Interfaces.Services
{
    public interface IAccountService
    {
        Task<IBaseResponse<AuthResponse>> Authenticate(AuthRequest request);

        Task<IBaseResponse<AuthResponse>> Register(RegisterRequest request);

        Task<IBaseResponse<TokenModel>> RefreshToken(TokenModel tokenModel);

        Task<IBaseResponse<bool>> RevokeUserToken(string username);

        Task<IBaseResponse<bool>> RevokeAllTokens();

        Task<IBaseResponse<IEnumerable<UserDto>>> GetUsers();

        Task<IBaseResponse<string>> ChangeUserRole(ChangeRoleRequest request);

    }
}
