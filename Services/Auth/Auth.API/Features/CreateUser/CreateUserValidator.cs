using FastEndpoints;
using FluentValidation;

namespace Auth.API.Features.CreateUser;

public class CreateUserValidator : Validator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();

        RuleFor(c => c.Email).NotEmpty()
            .EmailAddress();

        RuleFor(c => c.UserRoles).NotEmpty()
            .Must((c, roles) => AreUserRoleDatesValid(roles)).WithMessage("Invalid Roles");
    }


public static bool AreUserRoleDatesValid(IReadOnlyList<UserRoleDto> roles)
{
        return roles.All(role =>
        // StartDate must be today or in the future otherwise security concern
        (!role.StartDate.HasValue || role.StartDate.Value.Date >= DateTime.UtcNow) &&
        // Expiring date must be after StartDate or today if stardate is null
        (!role.ExpiringDate.HasValue || (role.StartDate ?? DateTime.UtcNow).Date < role.ExpiringDate.Value.Date)

        );
        
}

}