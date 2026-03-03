using Articles.Abstractions;
using MediatR;
using Submission.Domain.Entities;
using Submission.Persistence.Repositories;
using Blocks.EntityFramework;


namespace Submission.Application.Features.CreateArticle;

internal class CreateArticleCommandHandler(Repository<Journal> _journalRepository) : IRequestHandler<CreateArticleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken ct)
    {
        var journal = await _journalRepository.FindByIdOrThrowAsync(command.JournalId);
        var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);
        await _journalRepository.SaveChangesAsync(ct);

        return new IdResponse(article.Id);
    }
}

