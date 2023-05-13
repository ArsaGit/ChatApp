using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs;

public class CreateMessageDto
{
    [Required]
    public string? SenderId { get; set; }
    [Required]
    public int ChatRoomId { get; set; }
    [Required]
    public DateTime? DateCreated { get; set; } = DateTime.Now;
    [Required]
    public string? Text { get; set; }
}