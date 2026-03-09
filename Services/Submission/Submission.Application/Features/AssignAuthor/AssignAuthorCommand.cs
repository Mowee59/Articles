
using Blocks.Core.FluentValidation;

namespace Submission.Application.Features.AssignAuthor;

/// <summary>
/// Command to assign an author to an article with their contribution areas.
/// </summary>
/// <param name="AuthorId">Identifier of the author being assigned.</param>
/// <param name="IsCorrespondingAuthor">Indicates whether the author is the corresponding author.</param>
/// <param name="ContributionAreas">Set of contribution areas for the author on this article.</param>
public record AssignAuthorCommand(int AuthorId, bool IsCorrespondingAuthor, HashSet<ContributionArea> ContributionAreas)
    : ArticleCommand
{
    /// <summary>
    /// Action type representing assigning an author to an article.
    /// </summary>
    public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

/// <summary>
/// Validator for <see cref="AssignAuthorCommand"/> enforcing valid author id and contribution areas.
/// </summary>
public class AssignAuthorCommandValidator : ArticleCommandValidator<AssignAuthorCommand>
{
    public AssignAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .GreaterThan(0).WithMessageForInvalidId(nameof(AssignAuthorCommand.AuthorId));

        RuleFor(command => command.ContributionAreas)
            .NotEmptyWithMessage(nameof(AssignAuthorCommand.ContributionAreas));
    }
}