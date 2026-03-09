using Blocks.Core.Constraints;
using Blocks.Core.FluentValidation;

namespace Submission.Application.Features.CreateAndAssignAuthor;

/// <summary>
/// Command to assign an author to an article when the author may already exist
/// as a registered user or may need to be created from the provided details.
/// </summary>
/// <param name="UserId">Identifier of an existing user from the user service; null to create a new author from details.</param>
/// <param name="FirstName">First name of the author when creating a new author.</param>
/// <param name="LastName">Last name of the author when creating a new author.</param>
/// <param name="Email">Email address of the author when creating a new author.</param>
/// <param name="Title">Title or honorific of the author (e.g. Dr., Prof.).</param>
/// <param name="Affiliation">Institution or organization the author is affiliated with.</param>
/// <param name="IsCorrespondingAuthor">Indicates whether the author is the corresponding author.</param>
/// <param name="ContributionAreas">Set of contribution areas for the author on this article.</param>
public record CreateAndAssignAuthorCommand(int? UserId,
                                             string? FirstName,
                                             string? LastName,
                                             string? Email,
                                             string? Title,
                                             string? Affiliation,
                                             bool IsCorrespondingAuthor,
                                             HashSet<ContributionArea> ContributionAreas)
    : ArticleCommand
{
    /// <summary>
    /// Action type representing assigning an author (created or existing) to an article.
    /// </summary>
    public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

/// <summary>
/// Validator for <see cref="CreateAndAssignAuthorCommand"/> enforcing required
/// author details when creating a new author and valid contribution areas.
/// </summary>
public class CreateAndAssignAuthorCommandValidator : ArticleCommandValidator<CreateAndAssignAuthorCommand>
{
    public CreateAndAssignAuthorCommandValidator()
    {
        When(c => c.UserId == null, () =>
        {
            RuleFor(x => x.Email)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Email))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateAndAssignAuthorCommand.Email))
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.FirstName))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateAndAssignAuthorCommand.FirstName));

            RuleFor(x => x.LastName)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.LastName))
                .MaximumLengthWithMessage(MaxLength.C256, nameof(CreateAndAssignAuthorCommand.LastName));
            RuleFor(x => x.LastName)
                .MaximumLengthWithMessage(MaxLength.C32, nameof(CreateAndAssignAuthorCommand.Title));

            RuleFor(x => x.Affiliation)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Affiliation))
                .MaximumLengthWithMessage(MaxLength.C512, nameof(CreateAndAssignAuthorCommand.Affiliation));

        });

        RuleFor(x => x.ContributionAreas)
            .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.ContributionAreas));   

    }
}