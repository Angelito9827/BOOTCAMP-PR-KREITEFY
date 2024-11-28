using AutoMapper;
using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public class StyleService : GenericService<Style, StyleDto>, IStyleService
    {
        public StyleService(IStyleRepository styleRepository, IMapper mapper) : base(styleRepository, mapper)
        {
        }
    }
}
