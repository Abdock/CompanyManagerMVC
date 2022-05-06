using System.ComponentModel.DataAnnotations;

namespace CompanyManagerMVC.ViewModels;

#nullable disable

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email not set")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone not set")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Name not set")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Surname not set")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Password not set")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm your password")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Select department")]
    public string Department { get; set; }

    [Required(ErrorMessage = "Select user role")]
    public string Role { get; set; }
}