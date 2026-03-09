using Blocks.Core.FluentValidation;

namespace Submission.Application.Features.CreateArticle;

/// <summary>
/// Command to create a new article in a given journal.
/// </summary>
/// <param name="JournalId">Identifier of the journal the article belongs to.</param>
/// <param name="Title">Title of the article.</param>
/// <param name="Scope">Scope or abstract describing the article.</param>
/// <param name="ArticleType">Type of the article (e.g. research, review).</param>
public record CreateArticleCommand(int JournalId, string Title, string Scope, ArticleType ArticleType)
    : ArticleCommand
{
    /// <summary>
    /// Action type representing article creation.
    /// </summary>
      public override ArticleActionType ActionType => ArticleActionType.Create;
}


/// <summary>
/// Validator for <see cref="CreateArticleCommand"/> enforcing required title, scope,
/// and a valid journal identifier.
/// </summary>
public class CreateArticleCommandValidator : ArticleCommandValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmptyWithMessage(nameof(CreateArticleCommand.Title));
        RuleFor(x => x.Scope)
            .NotEmptyWithMessage(nameof(CreateArticleCommand.Scope));
        RuleFor(x => x.JournalId)
            .GreaterThan(0).WithMessageForInvalidId(nameof(CreateArticleCommand.JournalId));
    }
}
