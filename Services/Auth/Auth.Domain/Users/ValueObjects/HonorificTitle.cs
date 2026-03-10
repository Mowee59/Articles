using Auth.Domain.Users.Enums;
using Blocks.Core;
using Blocks.Domain.ValueObjects;

namespace Auth.Domain.Users.ValueObjects;

/// <summary>
/// Value object representing a user's honorific title (e.g., Mr, Dr, Prof).
/// Used within the authentication system to store and compare honorific titles in a normalized manner.
/// </summary>
public class HonorificTitle : StringValueObject
{
    private HonorificTitle(string value) => Value = value;

    public static HonorificTitle FromString(string honorific)
    {
        Guard.ThrowIfNullOrWhiteSpace(honorific);
        return new HonorificTitle(honorific);
    }

    public static HonorificTitle? FromEnum(Honorific? honorific)
    {
        if(honorific.HasValue)
            return new HonorificTitle(honorific!.ToString());

        return null;
    }
 
}
