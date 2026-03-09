
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

/// <summary>
/// Repository specialized for <see cref="Article"/> entities using the
/// Submission service's <see cref="SubmissionDbContext"/>.
/// </summary>
public class ArticleRepository(SubmissionDbContext dbContext)
    : Repository<Article>(dbContext)
{ 

}
