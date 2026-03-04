using Articles.Abstractions.Enums;
using Blocks.Domain;

namespace Submission.Domain.Entities;

public partial class Article
{
    public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor = false)
    {
        var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;

        if(Actors.Exists(a => a.Person.Id == author.Id && a.Role == role))
            throw new DomainException($"Author {author.EmailAdress} is already assigned to the article as {role}");

        Actors.Add(new ArticleAuthor()
        {
            ContributionAreas = contributionAreas,
            Person = author,
            Role = role

        });

        // TODO - Add domain event ArticleAuthorAssigned

    }
}
