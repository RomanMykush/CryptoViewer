using AutoMapper;
using CryptoViewer.Dtos;
using CryptoViewer.Models;

namespace CryptoViewer.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coin, CoinDto>().ReverseMap();
        CreateMap<Coin, SimpleCoinDto>().ReverseMap();
    }
}
