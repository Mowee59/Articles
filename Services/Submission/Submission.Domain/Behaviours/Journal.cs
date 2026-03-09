namespace Submission.Domain.Entities;

public partial class Journal 
{
    /// <summary>
    /// Creates a new article within this journal with the given metadata and
    /// initializes its stage to <see cref="ArticleStage.Created"/>.
    /// </summary>
    /// <param name="title">Title of the new article.</param>
    /// <param name="Type">Type of the article.</param>
    /// <param name="scope">Scope or abstract describing the article.</param>
    /// <returns>The newly created <see cref="Article"/> entity.</returns>
    public Article CreateArticle(string title, ArticleType Type, string scope )
    {
        var article = new Article()
        {
            Title = title,
            Type = Type,
            Scope = scope,
            JournalId = this.Id,
            Journal = this,
            Stage = ArticleStage.Created
        };
        _articles.Add(article);
        // TODO : add doomain event
        return article;
    }
}
