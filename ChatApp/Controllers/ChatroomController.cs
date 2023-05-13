using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatroomController : ControllerBase
{
    private readonly ChatAppContext _context;
    private readonly IMapper _mapper;

    public ChatroomController(ChatAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _context.Chatrooms.Where(r => !r.IsDeleted).ToListAsync();
        var roomDtos = _mapper.Map<ICollection<ChatroomDto>>(rooms);
        return Ok(roomDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoom([FromRoute] Guid id)
    {
        var room = await _context.Chatrooms.FindAsync(id);
        if (room is { IsDeleted: true }) return NotFound();
        
        var roomDto = _mapper.Map<ChatroomDto>(room);
        return Ok(roomDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom([FromBody] CreateChatroomDto chatroomDto)
    {
        var users = await _context.Users.Where(u => chatroomDto.UserIds.Contains(u.Id)).ToListAsync();
        var room = new Chatroom { Id = Guid.NewGuid(), Name = chatroomDto.Name, Users = users };
        _context.Chatrooms.Add(room);
        await _context.SaveChangesAsync();
        var roomDto = _mapper.Map<ChatroomDto>(room);
        return Created($"/{roomDto.Id}", roomDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRoom([FromRoute] Guid id)
    {
        var room = await _context.Chatrooms.FindAsync(id);
        if (room == null) return NotFound();
        
        room.IsDeleted = true;
        _context.Chatrooms.Update(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateRoom(
        [FromRoute] Guid id, 
        [FromBody] UpdateChatroomDto updateChatroomDto)
    {
        var room = await _context.Chatrooms.FindAsync(id);
        if (room == null) return NotFound();

        room.Name = updateChatroomDto.Name;
        _context.Chatrooms.Update(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}