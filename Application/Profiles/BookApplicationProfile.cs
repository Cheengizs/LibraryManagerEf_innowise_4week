using Application.Dto_s;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles;

public class BookApplicationProfile : Profile
{
    public BookApplicationProfile()
    {
        CreateMap<Book, BookResponse>().ReverseMap();
        CreateMap<BookRequest, Book>().ReverseMap();
    }
}