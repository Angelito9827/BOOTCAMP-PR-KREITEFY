using AutoMapper;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Application.Mapping
{
    public class ScoreMapperProfile : Profile
    {
        public ScoreMapperProfile()
        {
            CreateMap<Score, ScoreDto>();
            CreateMap<ScoreDto, Score>();
        }
    }
}
