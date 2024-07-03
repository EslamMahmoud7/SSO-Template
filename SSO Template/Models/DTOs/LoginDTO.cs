using System.ComponentModel.DataAnnotations;

namespace SSO_Template.DTOs.Models;

public class LoginDTO
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

}
