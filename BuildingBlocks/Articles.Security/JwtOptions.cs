using System.ComponentModel.DataAnnotations;

namespace Articles.Security;

public class JwtOptions
{
    [Required]
    public required string Issuer { get; init; }
    [Required]
    public required string Audience { get; init; }
    [Required]
    public required string Secret { get; init; }

    [Required]
    public int ValidForInMinutes { get; init; }
    public DateTime IssuedAt { get; set; } = DateTime.Now;
    public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForInMinutes);
    public DateTime Expiration => IssuedAt.Add(ValidFor);
}
