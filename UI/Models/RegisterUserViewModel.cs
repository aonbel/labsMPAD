using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class RegisterUserViewModel
{
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

    public IFormFile? ProfilePicture { get; set; }
}