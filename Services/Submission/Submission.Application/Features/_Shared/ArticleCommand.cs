
using Blocks.Core.FluentValidation;
using System.Text.Json.Serialization;

namespace Submission.Application.Features.Shared;

/// <summary>
/// Base record for commands that act on an article and are auditable actions.
/// Provides common metadata such as article id, comment, action type, and audit fields.
/// </summary>
/// <typeparam name="TActionType">Enum representing the type of article action.</typeparam>
/// <typeparam name="TResponse">Response type returned by the MediatR handler.</typeparam>
public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>, IRequest<TResponse>
    where TActionType : Enum
{
    /// <summary>
    /// Identifier of the target article for this command.
    /// </summary>
    [JsonIgnore]
    public int ArticleId { get; init; }

    /// <summary>
    /// Optional free-text comment associated with the action.
    /// </summary>
    public string? Comment { get; init; }

    /// <summary>
    /// Strongly typed action kind for the command.
    /// </summary>
    [JsonIgnore]
    public abstract TActionType ActionType {  get; }

    /// <summary>
    /// String representation of <see cref="ActionType"/> used for logging or serialization.
    /// </summary>
    [JsonIgnore]
    public string? Action => ActionType.ToString();

    /// <inheritdoc />
    [JsonIgnore]
    public DateTime CreatedOn => DateTime.UtcNow;

    /// <inheritdoc />
    [JsonIgnore]
    public int CreatedById { get; set; }
}

/// <summary>
/// Convenience base record for article commands that use <see cref="ArticleActionType"/>
/// as the action enum and <see cref="IdResponse"/> as the response type.
/// </summary>
public abstract record ArticleCommand : ArticleCommand<ArticleActionType, IdResponse>;

/// <summary>
/// Base validator for article commands that enforces a valid article identifier.
/// </summary>
/// <typeparam name="TFileActionCommand">Concrete article command type being validated.</typeparam>
public abstract class ArticleCommandValidator<TFileActionCommand> : AbstractValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(x => x.ArticleId)
            .GreaterThan(0).WithMessageForInvalidId(nameof(ArticleCommand.ArticleId));
    }
}