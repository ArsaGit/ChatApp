namespace ChatApp.DTOs;

public class MessageDto
{
    // public long Id { get; set; }
    public string? SenderName { get; set; }
    public DateTime? DateCreated { get; set; } = DateTime.Now;
    public string? Text { get; set; }
}