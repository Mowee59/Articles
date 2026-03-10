
namespace Auth.Domain.Users.Events;

public record UserCreated(User user, string resetPawwordToken);

