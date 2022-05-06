using System.ComponentModel.DataAnnotations;

namespace CompanyManagerMVC.ViewModels;

#nullable disable

public class LoginViewModel
{
    [Required(ErrorMessage = "Enter email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Enter password")]
    public string Password { get; set; }
}