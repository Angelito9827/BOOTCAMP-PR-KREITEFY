using bootcamp_framework.Infraestructure.Rest;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/artists")]
    [ApiController]
    [Authorize]
    public class ArtistsController : GenericCrudController<ArtistDto>
    {
        public ArtistsController(IArtistService artistService) : base(artistService)
        {
        }
    }
}
