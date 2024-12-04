using System.Text.RegularExpressions;

namespace bootcamp_pr_kreitefy_api.Application.Validators
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
        }
    }
}
