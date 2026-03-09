using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;


namespace Submission.Application.Features.CreateArticle;

/// <summary>
/// Handles <see cref="CreateArticleCommand"/> requests by creating a new article
/// in the target journal and optionally assigning the current user as an author.
/// </summary>
internal class CreateArticleCommandHandler(Repository<Journal> _journalRepository) : IRequestHandler<CreateArticleCommand, IdResponse>
{
    /// <summary>
    /// Loads the journal, creates a new article from the command data, attempts to
    /// assign the current user as an author, saves changes, and returns the article id.
    /// </summary>
    /// <param name="command">The create-article command.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An <see cref="IdResponse"/> containing the created article id.</returns>
    public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken ct)
    {
        var journal = await _journalRepository.FindByIdOrThrowAsync(command.JournalId);

        var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);

        await AssignCurrentUserAsAuthor(article, command);

        await _journalRepository.SaveChangesAsync(ct);

        return new IdResponse(article.Id);
    }

    /// <summary>
    /// Attempts to assign the current user (from <see cref="CreateArticleCommand.CreatedById"/>)
    /// as the corresponding author with an initial contribution area, if an author record exists.
    /// </summary>
    /// <param name="article">The article just created.</param>
    /// <param name="command">The create-article command containing the creator id.</param>
    private async Task AssignCurrentUserAsAuthor(Article article, CreateArticleCommand command)
    {
        var author = await _journalRepository.Context.Authors.SingleOrDefaultAsync(t => t.UserId == command.CreatedById);

        if(author is not null)
        {
            article.AssignAuthor(author, [ContributionArea.OriginalDraft], isCorrespondingAuthor: true);
        }
    }
}

