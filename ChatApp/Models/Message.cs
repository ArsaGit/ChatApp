using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models;

public class Message
{
    public Guid Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public string? Text { get; set; }
    
    [ForeignKey(nameof(User))]
    public Guid? SenderId { get; set; }
    public User? Sender { get; set; }
    [ForeignKey(nameof(Chatroom))]
    public Guid? RoomId { get; set; }
    public Chatroom? Room { get; set; }
}