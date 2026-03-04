using Blocks.Core;
using System.Text.RegularExpressions;

namespace Submission.Domain.ValueObjects;

public class EmailAdress
{
    public string Value { get; private set; }

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
     public override string ToString() => Value;
     public override bool Equals(object? obj)
     {
         if (obj is EmailAdress other)
             return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
         return false;
     }
     public override int GetHashCode() => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
}
