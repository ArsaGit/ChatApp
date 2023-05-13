using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Contexts;

public class ChatAppContext : DbContext
{
    public DbSet<Chatroom> Chatrooms { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options)
    {
        
    }
}