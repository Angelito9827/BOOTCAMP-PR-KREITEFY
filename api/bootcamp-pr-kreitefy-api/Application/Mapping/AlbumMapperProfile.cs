using AutoMapper;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Application.Mapping
{
    public class AlbumMapperProfile : Profile
    {
        public AlbumMapperProfile()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<AlbumDto, Album>();
        }
    }
}
