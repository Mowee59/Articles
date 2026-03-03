using Articles.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Submission.Domain.Entities;

public partial class Journal
{
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
