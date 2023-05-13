namespace ChatApp.Models;

public class IndexViewModel
{
    public User? CurrentUser { get; set; }
    public List<ChatRoom>? ChatRooms { get; set; } = new List<ChatRoom>();
}