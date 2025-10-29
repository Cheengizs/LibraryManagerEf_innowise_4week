using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure.MapProfiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorEntity>().ReverseMap();
    }
}