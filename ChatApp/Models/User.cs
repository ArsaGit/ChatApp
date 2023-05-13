using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models;

public class User : IdentityUser
{
    public bool IsDeleted { get; set; }
    
    public List<ChatRoom>? ChatRooms { get; set; } = new List<ChatRoom>();
}