using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;

namespace ChatApp;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Chatroom, ChatroomDto>();
        CreateMap<Message, MessageDto>();
        
        CreateMap<CreateUserDto, User>();
        CreateMap<CreateChatroomDto, Chatroom>();
        CreateMap<CreateMessageDto, Message>();
    }
}