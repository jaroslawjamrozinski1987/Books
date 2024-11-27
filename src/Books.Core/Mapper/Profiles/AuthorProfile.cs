using AutoMapper;
using Books.Core.Data.Models;
using Books.Core.DTO;

namespace Books.Core.Mapper.Profiles
{
    internal class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDTO, Author>().ReverseMap();
        }
    }
}
