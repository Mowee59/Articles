using Blocks.Domain.Entities;
using Blocks.EntityFramework;

namespace Submission.Persistence.Repositories;

/// <summary>
/// Submission-specific repository wrapper for entities using the <see cref="SubmissionDbContext"/>.
/// Inherits the generic EF Core repository behavior with the concrete context type.
/// </summary>
public class Repository<TEntity>(SubmissionDbContext dbContext)
    : Repository<SubmissionDbContext, TEntity>(dbContext)
    where TEntity : class, IEntity
{
}
