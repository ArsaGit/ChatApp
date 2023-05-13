using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models;

public class Message
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string? Text { get; set; }
    
    [ForeignKey(nameof(User))]
    public string? SenderId { get; set; }
    public string? SenderName { get; set; }
    public User? Sender { get; set; }

    [ForeignKey(nameof(ChatRoom))]
    public int ChatRoomId { get; set; }
    public ChatRoom? ChatRoom { get; set; }
}