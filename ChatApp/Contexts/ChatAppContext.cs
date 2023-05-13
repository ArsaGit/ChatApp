using ChatApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Contexts;

public class ChatAppContext : IdentityDbContext<User>
{
    public DbSet<ChatRoom> ChatRooms { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options)
    {
        
    }
}