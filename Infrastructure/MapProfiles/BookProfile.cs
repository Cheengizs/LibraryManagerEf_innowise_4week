using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure.MapProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookEntity>().ReverseMap();
    }
}