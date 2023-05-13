using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs;

public class UpdateUserDto
{
    [Required]
    public string? Name { get; set; }
}