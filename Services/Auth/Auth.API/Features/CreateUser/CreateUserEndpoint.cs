using Articles.Abstractions.Enums;
using Auth.Domain.Users;
using Auth.Domain.Users.Events;
using Blocks.Exceptions;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;



namespace Auth.API.Features.CreateUser;

[Authorize(Roles = Role.USERADMIN)]
[HttpPost("users")]
public class CreateUserEndpoint(UserManager<User> userManager) 
    : Endpoint<CreateUserCommand, CreateUserResponse>
{
    public override async Task HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if(user is not null)
            throw new BadRequestException($"User with email {command.Email} already exists");

        user = Auth.Domain.Users.User.Create(command);

        var result = await userManager.CreateAsync(user);
        if(!result.Succeeded)
        {
            var errorMessages = string.Join(" | ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            throw new BadRequestException($"Unable to create user: {errorMessages}");
        }

        var resetPawwordToken = await userManager.GeneratePasswordResetTokenAsync(user);

        await PublishAsync(new UserCreated(user, resetPawwordToken));

        await Send.OkAsync(new CreateUserResponse(command.Email, user.Id, resetPawwordToken));

    }
}
