using AutoMapper;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Application.Mapping
{
    public class StyleMapperProfile : Profile
    {
        public StyleMapperProfile()
        {
            CreateMap<Style, StyleDto>();
            CreateMap<StyleDto, Style>();
        }
    }
}
