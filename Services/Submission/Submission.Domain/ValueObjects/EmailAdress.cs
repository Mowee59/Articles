using Blocks.Core;
using Blocks.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Submission.Domain.ValueObjects;

public class EmailAdress : StringValueObject
{
 

    private EmailAdress(string value) => Value = value;

    public static EmailAdress Create(string email)
    {
        Guard.ThrowIfNullOrWhiteSpace(email);
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email address format.");

        return new EmailAdress(email);
    }

    private static bool IsValidEmail(string email)
    {
       const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
       return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
    }

}
