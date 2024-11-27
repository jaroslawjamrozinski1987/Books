using AutoMapper;
using Books.Core.Data.Models;
using Books.Core.DTO;

namespace Books.Core.Mapper.Profiles
{
    public class OrderLineProfile : Profile
    {
        public OrderLineProfile()
        {
            CreateMap<OrderLine, OrderLineDTO>().ReverseMap();
        }
    }
}
