using AutoMapper;
using ToysNGames.api.DTOS;
using ToysNGames.Data.Entities;

namespace ToysNGames.api.Profiles
{
    public class ProductProfiler : Profile
    {
        public ProductProfiler()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(c => c.Price, o => o.MapFrom(m => m.Price))
                .ForMember(c => c.AgeRestriction, o => o.MapFrom(m => m.AgeRestriction))
                .ReverseMap();
        }

    }
}
