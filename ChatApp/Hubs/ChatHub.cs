using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly ChatAppContext _chatContext;
    private readonly IMapper _mapper;

    public ChatHub(ChatAppContext chatContext, IMapper mapper)
    {
        _chatContext = chatContext;
        _mapper = mapper;
    }

    public async Task SendInvite(string userName, string roomName)
    {
        var user = await _chatContext.Users.FirstAsync(u => u.UserName == userName);
        var room = await _chatContext.ChatRooms.FirstAsync(r => r.Name == roomName);
        await Clients.User(user.Id).SendAsync("ReceiveInvite", room.Id, roomName);
    }

    public async Task SendMessage(string roomName, Message message)
    {
        var messageDto = _mapper.Map<MessageDto>(message);
        await Clients.Group(roomName).SendAsync("ReceiveMessage", messageDto.SenderName, messageDto.Text);
    }

    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }
    
    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }
}