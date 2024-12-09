using AutoMapper;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Application.Mapping
{
    public class SongMapperProfile : Profile
    {
        public SongMapperProfile()
        {
            CreateMap<Song, SongDto>()
                .ForMember(dest => dest.StyleName, opt => opt.MapFrom(src => src.Style.Name))
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(dest => dest.AlbumImage, opt => opt.MapFrom(src => Convert.ToBase64String(src.Album.Image)));

            CreateMap<Song, RecentSongDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.AlbumImage, opt => opt.MapFrom(src => Convert.ToBase64String(src.Album.Image)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<SongDto, Song>();
        }
    }
}
