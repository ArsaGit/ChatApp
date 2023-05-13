using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ChatAppContext _context;
    private readonly IMapper _mapper;

    public MessageController(ChatAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await _context.Messages.ToListAsync();
        var messageDtos = _mapper.Map<ICollection<MessageDto>>(messages);
        return Ok(messageDtos);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessage([FromRoute] Guid id)
    {
        var message = await _context.Messages.FindAsync(id);
        var messageDto = _mapper.Map<MessageDto>(message);
        return Ok(messageDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddMessage([FromBody] CreateMessageDto messageDto)
    {
        var message = _mapper.Map<Message>(messageDto);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return Created($"/{message.Id}", message);
    }
}