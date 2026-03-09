namespace Submission.Domain.Entities;

/// <summary>
/// Domain entity representing an author, extending <see cref="Person"/> with
/// academic details such as degree and discipline.
/// </summary>
public partial class Author : Person
{
    /// <summary>
    /// Academic degree held by the author (e.g. PhD, MD).
    /// </summary>
    public string? Degree { get; set; }
    /// <summary>
    /// Academic or professional discipline of the author.
    /// </summary>
    public string? Discipline { get; set; }
}
