using System.ComponentModel.DataAnnotations;

namespace Gamesmarket.Domain.ViewModel.Identity
{
    public class RegisterRequest
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; } = null!;

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

    }
}