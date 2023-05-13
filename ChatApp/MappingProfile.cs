using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;

namespace ChatApp;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ChatRoom, ChatroomDto>();
        CreateMap<Message, MessageDto>();
        // CreateMap<Message, MessageDto>()
        //     .ForMember(dest => dest.SenderName,
        //         opt => 
        //             opt.MapFrom(src => src.Sender.UserName));
        
        CreateMap<CreateChatroomDto, ChatRoom>();
        CreateMap<CreateMessageDto, Message>();
    }
}