using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUser
{
    public record GetAdminUserResponse(
        Guid Id,
        Guid? KeyCloakId,
        string Email,
        string FirstName,
        string LastName,
        string Title,
        Guid? TeamId,
        AdminUserStatus Status,
        string? PhoneNumber,
        string? AvatarUrl,
        DateTime ContractStartDate,
        DateTime? ContractEndDate,
        DateTime? OffboardedAt,
        bool MfaEnabled);
}
