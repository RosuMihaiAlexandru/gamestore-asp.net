namespace Gamesmarket.Domain.Entity
{
    public class Order
    {
        public long Id { get; set; }

        public long? GameId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public long? CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
