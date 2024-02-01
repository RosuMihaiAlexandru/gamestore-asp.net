namespace Gamesmarket.Domain.Entity
{
    public class Cart
    {
        public long Id { get; set; }

        public User User { get; set; }

        public long UserId { get; set; }

        public List<Order> Orders { get; set; }
    }
}
