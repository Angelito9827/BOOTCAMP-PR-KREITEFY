using bootcamp_framework.Infraestructure.Rest;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/albums")]
    [ApiController]
    [Authorize]
    public class AlbumsController : GenericCrudController<AlbumDto>
    {
        public AlbumsController(IAlbumService albumService) : base(albumService)
        {
        }
    }
}
