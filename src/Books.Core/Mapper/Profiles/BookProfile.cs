using AutoMapper;
using Books.Core.Data.Models;
using Books.Core.DTO;

namespace Books.Core.Mapper.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ForMember(a=>a.Id, b=>b.MapFrom(z=>z.BookId)).ReverseMap();
            CreateMap<Book, Book>();
        }
    }
}
