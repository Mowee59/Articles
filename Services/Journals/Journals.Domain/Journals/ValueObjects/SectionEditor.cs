using Journals.Domain.Journals.Enums;
using Redis.OM.Modeling;

namespace Journals.Domain.Journals.ValueObjects;


// TODO - Could be useful and more consistent to implement ValueObject interface
[Document]

public class SectionEditor
{
    public int EditorId { get; set; }
    public EditorRole EditorRole { get; set; }
}
