using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs;

public class CreateUserDto
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Name { get; set; }
}