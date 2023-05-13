using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ChatAppContext _context;
    private readonly IMapper _mapper;

    public MessageController(ChatAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet("/Api/Messages")]
    public async Task<IActionResult> GetMessages(int roomId)
    {
        var messages = await _context.Messages
            .Where(m => m.ChatRoomId.Equals(roomId)).OrderByDescending(m=>m.DateCreated)
            .ToListAsync();
        var messageDtos = _mapper.Map<ICollection<MessageDto>>(messages);
        return Ok(messageDtos);
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetMessage([FromRoute] long id)
    {
        var message = await _context.Messages.FindAsync(id);
        var messageDto = _mapper.Map<MessageDto>(message);
        return Ok(messageDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddMessage([FromBody] CreateMessageDto createMessageDto)
    {
        var message = _mapper.Map<Message>(createMessageDto);
        var user = await _context.Users.FindAsync(message.SenderId);
        message.SenderName = user.UserName;
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}