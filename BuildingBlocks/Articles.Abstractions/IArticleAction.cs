

using Blocks.Domain;

namespace Articles.Abstractions;

/// <summary>
/// Describes an auditable action performed on a specific article.
/// </summary>
public interface IArticleAction : IAuditableAction
{
    int ArticleId { get; }
}

/// <summary>
/// Article-related action with a strongly typed action kind.
/// </summary>
/// <typeparam name="TActionType">Enum describing the type of article action.</typeparam>
public interface IArticleAction<TActionType> : IArticleAction, IAuditableAction<TActionType>
    where TActionType : Enum
{
}