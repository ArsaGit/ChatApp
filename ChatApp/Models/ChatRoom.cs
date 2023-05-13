using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models;

public class ChatRoom
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsDeleted { get; set; } = false;

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}