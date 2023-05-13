namespace ChatApp.Models;

public class Chatroom
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public bool IsDeleted { get; set; }
}