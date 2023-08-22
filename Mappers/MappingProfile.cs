using AutoMapper;
using qwerty_chat_api.DTOs;
using qwerty_chat_api.Models;

namespace qwerty_chat_api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LoginRequest, User>();
            CreateMap<RegisterRequest, User>();
        }
    }
}
