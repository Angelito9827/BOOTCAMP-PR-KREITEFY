namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }
}
