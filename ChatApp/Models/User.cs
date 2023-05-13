namespace ChatApp.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Login { get; set; }
    public ICollection<Chatroom> Chatrooms { get; set; } = new List<Chatroom>();
    public bool IsDeleted { get; set; }
}