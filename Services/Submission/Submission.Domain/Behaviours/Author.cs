using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Author : Person
{
   /// <summary>
   /// Factory method to create a new <see cref="Author"/> aggregate with the required
   /// contact and affiliation details.
   /// </summary>
   /// <param name="email">Email address of the author.</param>
   /// <param name="firstName">First name of the author.</param>
   /// <param name="lastName">Last name of the author.</param>
   /// <param name="affiliation">Institution or organization the author is affiliated with.</param>
   /// <param name="title">Optional title or honorific (e.g. Dr., Prof.).</param>
   /// <returns>A fully initialized <see cref="Author"/> instance.</returns>
   public static Author Create(string email,
                               string firstName,
                               string lastName,
                               string affiliation,
                               string? title
                               )
   {
       var author = new Author
       {
           EmailAdress = EmailAdress.Create(email),
           FirstName = firstName,
           LastName = lastName,
           Afiliation = affiliation,
           Title = title,
       };

        //TODO - Create Domain event AuthorCreated

        return author;
   }
}
