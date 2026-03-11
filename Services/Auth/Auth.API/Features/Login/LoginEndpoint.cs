using Articles.Security;
using Auth.Application;
using Blocks.AspNetCore.Extensions;
using Blocks.Core.Extensions;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.API.Features.Login;

[AllowAnonymous]
[HttpPost("/login")]
public class LoginEndpoint(UserManager<User> _userManager,
                           SignInManager<User> _signInManager,
                           TokenFactory _tokenFactory) : Endpoint<LoginCommand, LoginResponse>
{
    public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
            throw new BadRequestException($"User not found {command.Email}"); 

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
            throw new BadRequestException($"Invalid credentials for {command.Email}");

        var userRoles = await _userManager.GetRolesAsync(user);

        // generate tokens
        var jwtToken = _tokenFactory.GenerateJWTToken(user.Id.ToString(), user.FullName, command.Email, userRoles, Array.Empty<Claim>());
        var refreshToken = _tokenFactory.GenerateRefreshToken(HttpContext.GetClientIpAddress());

        user.AddRefreshToken(refreshToken);

        await Send.OkAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token));
    }

   
}
