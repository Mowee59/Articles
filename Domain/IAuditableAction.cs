
namespace Blocks.Domain;

/// <summary>
/// Represents an action in the domain that should be tracked for auditing.
/// Captures who performed the action and when.
/// </summary>
public interface IAuditableAction
{
    /// <summary>
    /// UTC timestamp when the action was created.
    /// Defaults to <see cref="DateTime.UtcNow"/>.
    /// </summary>
    public DateTime CreatedOn => DateTime.UtcNow;

    /// <summary>
    /// Identifier of the user or actor that created the action.
    /// </summary>
    public int CreatedById { get; set; }
}

/// <summary>
/// Auditable action with a strongly typed action kind (e.g. Created, Updated, Deleted).
/// </summary>
/// <typeparam name="TActionType">Enum describing the type of action.</typeparam>
public interface IAuditableAction<TActionType> : IAuditableAction
    where TActionType : Enum
{
    /// <summary>
    /// Domain-specific type of the action being audited.
    /// </summary>
    TActionType ActionType { get; }
}
