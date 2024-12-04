using System.Text.RegularExpressions;

namespace bootcamp_pr_kreitefy_api.Application.Validators
{
    public class PasswordValidator
    {
        public static IEnumerable<string> ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                yield return "Password cannot be empty.";

            if (password.Length < 8)
                yield return "Password must be at least 8 characters long.";

            if (!Regex.IsMatch(password, @"[A-Z]"))
                yield return "Password must contain at least one uppercase letter.";

            if (!Regex.IsMatch(password, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter.";

            if (!Regex.IsMatch(password, @"[0-9]"))
                yield return "Password must contain at least one number.";

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>_-]"))
                yield return "Password must contain at least one special character.";
        }
    }
}
