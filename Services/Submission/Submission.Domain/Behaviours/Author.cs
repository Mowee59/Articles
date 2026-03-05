using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Author : Person
{
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
