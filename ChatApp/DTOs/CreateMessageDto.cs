using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs;

public class CreateMessageDto
{
    [Required]
    public Guid? SenderId { get; set; }
    [Required]
    public Guid? RoomId { get; set; }
    [Required]
    public DateTime? DateCreated { get; set; }
    [Required]
    public string? Text { get; set; }
}