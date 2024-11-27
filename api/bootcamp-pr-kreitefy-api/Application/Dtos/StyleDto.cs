using bootcamp_framework.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class StyleDto : IDto
    {
        public long Id { get; set; }

        public required string Name { get; set; }
    }
}
