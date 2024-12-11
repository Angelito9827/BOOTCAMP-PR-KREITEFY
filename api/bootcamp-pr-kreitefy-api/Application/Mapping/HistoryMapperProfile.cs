using AutoMapper;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Application.Mapping
{
    public class HistoryMapperProfile : Profile
    {
        public HistoryMapperProfile()
        {
            CreateMap<History, HistoryDto>();
            CreateMap<HistoryDto, History>();

            CreateMap<History, HistoryProfileDto>()
                .ForMember(dest => dest.SongName, opt => opt.MapFrom(src => src.Song.Name))
                .ForMember(dest => dest.SongId, opt => opt.MapFrom(src => src.Song.Id))
                .ForMember(dest => dest.PlayedAtFormatted, opt => opt.MapFrom(src => src.PlayedAt));
        }
    }
}
