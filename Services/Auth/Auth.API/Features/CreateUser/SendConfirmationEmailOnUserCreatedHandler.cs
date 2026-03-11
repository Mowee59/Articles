using Auth.Domain.Users;
using Auth.Domain.Users.Events;
using Blocks.AspNetCore.Extensions;
using EmailService.Contracts;
using FastEndpoints;
using Microsoft.Extensions.Options;
using Flurl;

namespace Auth.API.Features.CreateUser;

public class SendConfirmationEmailOnUserCreatedHandler(IEmailService emailService,
                                                       HttpContextAccessor httpContextAccessor,
                                                       IOptions<EmailOptions> emailOptions) 
    : IEventHandler<UserCreated>
{
    public async Task HandleAsync(UserCreated eventModel, CancellationToken ct)
    {

        var url = httpContextAccessor.HttpContext?.Request.BaseUrl()
            .AppendPathSegment("password")
            .SetQueryParams(new { eventModel.resetPawwordToken });

        var emailMessage = BuildConfirmationEmail(eventModel.user, url, emailOptions.Value.EmailFromAddress);
        await emailService.SendEmailAsync(emailMessage, ct);
    }
    // TODO - implement real reset link instead of using a link to our API
    public EmailMessage BuildConfirmationEmail(User user, string resetLink, string fromEmailAddress)
    {
        
        const string ConfirmationEmail =
            "Dear {0},<br/>An account has been created for you.<br/>Please set your password using the following URL: <br/>{1}";

        return new EmailMessage(
            "Your Account Has Been Created – Set Your Password",
            new Content(ContentType.Html, string.Format(ConfirmationEmail, user.FullName, resetLink)),
            new EmailAddress("Articles", fromEmailAddress),
            new List<EmailAddress> { new EmailAddress(user.FullName, user.Email!) }
            );

    }

}
