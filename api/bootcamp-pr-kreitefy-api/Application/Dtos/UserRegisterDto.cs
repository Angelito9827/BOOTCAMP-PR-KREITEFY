namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class UserRegisterDto
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
    }
}
