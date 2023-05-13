namespace ChatApp.DTOs;

public class MessageDto
{
    public Guid Id { get; set; }
    public UserDto? Sender { get; set; }
    public DateTime? DateCreated { get; set; }
    public string? Text { get; set; }
}