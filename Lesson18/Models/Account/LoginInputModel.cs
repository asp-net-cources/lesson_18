using System.ComponentModel.DataAnnotations;

namespace Lesson18.Models.Account;

public class LoginInputModel
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}