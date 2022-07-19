using AutoMapper;
using VidlyAPI.Models;
using VidlyAPI.Models.DTO;

namespace Vidly
{
    public class VidlyMapping : Profile
    {
        public VidlyMapping()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<MemberShipTypeDto, MemberShipType>().ReverseMap();
            CreateMap<MovieDto, Movie>().ReverseMap();
            CreateMap<GenreDto, Genre>().ReverseMap();
        }
    }
}
