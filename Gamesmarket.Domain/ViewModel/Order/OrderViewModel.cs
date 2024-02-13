namespace Gamesmarket.Domain.ViewModel.Order
{
    public class OrderViewModel
    {
        public long Id { get; set; }

        public string GameName { get; set; }

        public string GameDeveloper { get; set; }

        public string GameGenre { get; set; }

        public string GamePrice { get; set; }

        public string ImagePath { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string DateCreate { get; set; }
    }
}