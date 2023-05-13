using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs;

public class UpdateChatroomDto
{
    [Required]
    public string? Name { get; set; }
}