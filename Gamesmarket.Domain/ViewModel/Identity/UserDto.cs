namespace Gamesmarket.Domain.ViewModel.Identity
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}