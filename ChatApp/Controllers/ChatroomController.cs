using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers;

[ApiController]
[Route("/Api/[controller]")]
public class ChatroomController : ControllerBase
{
    private readonly ChatAppContext _context;
    private readonly IMapper _mapper;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatroomController(ChatAppContext context, IMapper mapper, IHubContext<ChatHub> hubContext)
    {
        _context = context;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _context.ChatRooms.Where(r => !r.IsDeleted).ToListAsync();
        var roomDtos = _mapper.Map<ICollection<ChatroomDto>>(rooms);
        return Ok(roomDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoom([FromRoute] int id)
    {
        var room = await _context.ChatRooms.FindAsync(id);
        if (room is { IsDeleted: true }) return NotFound();
        
        var roomDto = _mapper.Map<ChatroomDto>(room);
        return Ok(roomDto);
    }

    [HttpPost]
    [Route("AddRoom")]
    public async Task<RedirectResult> AddRoom([FromBody] CreateChatroomDto chatroomDto)
    {
        var user = await _context.Users.FindAsync(chatroomDto.UserId);
        
        if (!string.IsNullOrEmpty(chatroomDto.Name))
        {
            var chatRoom = new ChatRoom
            {
                Name = chatroomDto.Name,
                IsDeleted = false,
                
            };

            if (user != null) chatRoom.Users.Add(user);

            var addedRoom = _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            // await _hubContext.Clients.Users(user.Id)
            //     .SendAsync("ReceiveInvite", addedRoom.Entity.Id, chatroomDto.Name);
        }

        return Redirect("/");
    }
    
    [HttpPost]
    [Route("InviteUser")]
    public async Task<RedirectResult> InviteUser([FromBody] InviteUserDto inviteUserDto)
    {
        var user1 = await _context.Users.FirstOrDefaultAsync(u => u.UserName == inviteUserDto.UserName);
        var userId = user1.Id;
        var chatRoom = await _context.ChatRooms.FindAsync(inviteUserDto.RoomId);

        if (chatRoom != null && !string.IsNullOrEmpty(userId))
        {
            var user2 = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (user2 != null)
            {
                chatRoom.Users.Add(user2);
                await _context.SaveChangesAsync();
            }
        }

        return Redirect("/");
    }
}