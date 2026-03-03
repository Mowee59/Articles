using System;
using System.Collections.Generic;
using System.Text;
using Articles.Abstractions;
using Articles.Abstractions.Enums;
using FluentValidation;
using MediatR;

namespace Submission.Application.Features.CreateArticle;

public record CreateArticleCommand(int JournalId, string Title, string Scope, ArticleType ArticleType) : IRequest<IdResponse>
{

}


public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty");
        RuleFor(x => x.Scope)
            .NotEmpty().WithMessage("Scope cannot be empty");
        RuleFor(x => x.JournalId)
            .GreaterThan(0).WithMessage("JournalId must be greater than 0");
    }
}
