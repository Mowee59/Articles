using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;


namespace Submission.Persistence;

/// <summary>
/// Entity Framework Core DbContext for the Submission service, exposing sets for
/// articles, journals, people, authors, actors, assets, and asset type definitions.
/// </summary>
public class SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : DbContext(options)
{
    #region Entities
    /// <summary>
    /// Articles submitted to journals.
    /// </summary>
    public virtual DbSet<Article> Articles { get; set; }
    /// <summary>
    /// Journals that accept submissions.
    /// </summary>
    public virtual DbSet<Journal> Journals { get; set; }
    /// <summary>
    /// People involved in submissions (base type for authors, reviewers, etc.).
    /// </summary>
    public virtual DbSet<Person> Persons { get; set; }
    /// <summary>
    /// Authors participating in article submissions.
    /// </summary>
    public virtual DbSet<Author> Authors { get; set; }
    /// <summary>
    /// Actor links between people and articles with specific roles.
    /// </summary>
    public virtual DbSet<ArticleActor> ArticleActors { get; set; }
    /// <summary>
    /// Logical assets attached to articles (e.g. manuscript, supplementary files).
    /// </summary>
    public virtual DbSet<Asset> Assets { get; set; }
    /// <summary>
    /// Definitions describing how asset types behave and are stored.
    /// </summary>
    public virtual DbSet<AssetTypeDefinition> AssetTypes{ get; set; }
    #endregion

    /// <summary>
    /// Applies entity configurations from the current assembly and the base model configuration.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the EF model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
       modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
