using Application.Dto_s;
using Application.Dto_s.Book;
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