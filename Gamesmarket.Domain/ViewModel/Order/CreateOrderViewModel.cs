using System.ComponentModel.DataAnnotations;

namespace Gamesmarket.Domain.ViewModel.Order
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Specify Email")]
        [MinLength(5, ErrorMessage = "Email must have more than 5 characters")]
        [MaxLength(30, ErrorMessage = "Email must have less than 30 characters")]
        public string Email { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please provide a name")]
        [MaxLength(20, ErrorMessage = "Name must be less than 20 characters long")]
        [MinLength(3, ErrorMessage = "Name must be longer than 3 characters")]
        public string Name { get; set; }

        public long GameId { get; set; }

        public string Login { get; set; }
    }
}