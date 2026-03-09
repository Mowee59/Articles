using Articles.Abstractions.Enums;
using Blocks.Domain;

namespace Submission.Domain.Entities;

public partial class Article
{
    /// <summary>
    /// Assigns an author to the article with the given contribution areas and role
    /// (corresponding author or regular author). Throws if the same author is already
    /// assigned with the same role.
    /// </summary>
    /// <param name="author">Author to assign to the article.</param>
    /// <param name="contributionAreas">Contribution areas for this author on the article.</param>
    /// <param name="isCorrespondingAuthor">Indicates whether the author is the corresponding author.</param>
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


    /// <summary>
    /// Creates a new asset of the specified type for this article, enforcing the
    /// maximum number of allowed assets for that type.
    /// </summary>
    /// <param name="type">Asset type definition for the new asset.</param>
    /// <returns>The created asset.</returns>
    public Asset CreateAsset(AssetTypeDefinition type)
    {
        var assetCount = _assets
            .Where(a => a.Type == type.Id)
            .Count();

        if (type.MaxAssetCount > assetCount - 1)
            throw new DomainException($"The maximum number of files allowed for {type.Name.ToString()} was already reached");

        var asset = Asset.Create(this, type);
        _assets.Add(asset);

        return asset;

    }
}
