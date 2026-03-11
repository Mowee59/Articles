using Articles.Security;
using Auth.Domain.Users;
using Blocks.Core.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application;

public class TokenFactory
{

    private readonly JwtOptions _jwtOptions;

    public TokenFactory(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public RefreshToken GenerateRefreshToken(string clientIpAddress)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                CreatedOn = DateTime.UtcNow,
                CreatedByIp = clientIpAddress,
            };
        }
    }

    public string GenerateJWTToken(string userId, string fullName, string email, IEnumerable<string> roles, IEnumerable<Claim> additionalClaims)
    {
        
        var claims = new[]
        {
            // Standard JWT claims
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),

            // Specific to Microsoft applications
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, fullName),
            new Claim(ClaimTypes.Email, email)
        }
        .Concat(roles.Select(r => new Claim(ClaimTypes.Role, r))) // Adding Claims for roles
        .Concat(additionalClaims); // Adding additional Claims

        var secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_jwtOptions.Secret));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            notBefore: DateTime.UtcNow,
            expires: _jwtOptions.Expiration,
            claims: claims,
            signingCredentials: signinCredentials);

        var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return encodedJwtToken;
    }

}
