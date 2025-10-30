using Application.Dto_s.Author;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles;

public class AuthorApplicationProfile : Profile
{
    public AuthorApplicationProfile()
    {
        CreateMap<Author, AuthorResponse>().ReverseMap();
        CreateMap<Author, AuthorRequest>().ReverseMap();
    }
}