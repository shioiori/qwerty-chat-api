using AutoMapper;
using qwerty_chat_api.Application.DTOs;
using qwerty_chat_api.Infrastructure.Models;

namespace qwerty_chat_api.Domain.Mappers
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
