using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs;

public class CreateChatroomDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public ICollection<Guid>? UserIds { get; set; }
}