using Mapster;

namespace Auth.Domain.Users;

public partial class UserRole
{
    public static UserRole Create(IUserRole userRoleInfo)
    {
        var now = DateTime.UtcNow;

        // StartDate must be today or in the future
        if (userRoleInfo.StartDate.HasValue && userRoleInfo.StartDate.Value < now)
            throw new ArgumentException("StartDate must be today or in the future.", nameof(userRoleInfo.StartDate));

        // ExpiringDate must be after StartDate
        if (userRoleInfo.ExpiringDate.HasValue && userRoleInfo.StartDate.HasValue && userRoleInfo.StartDate.Value >= userRoleInfo.ExpiringDate.Value)
            throw new ArgumentException("ExpiringDate must be after StartDate.", nameof(userRoleInfo.ExpiringDate));
        
        // Map the user role info to the user role
        var userRole = userRoleInfo.Adapt<UserRole>();

        return userRole;
    }
}
