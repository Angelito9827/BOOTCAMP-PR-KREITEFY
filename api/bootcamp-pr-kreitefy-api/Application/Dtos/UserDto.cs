using bootcamp_framework.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class UserDto : IDto
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public string Password { get; set; }

        public long RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
