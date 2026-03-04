

using Blocks.Domain;

namespace Articles.Abstractions;

public interface IArticleAction : IAuditableAction
{
    int ArticleId { get; }
}

public interface IArticleAction<TActionType> : IArticleAction, IAuditableAction<TActionType>
    where TActionType : Enum
{
}