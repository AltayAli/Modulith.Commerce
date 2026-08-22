namespace Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers
{
    public sealed record AdminUserCreatedIntegrationEvent(
    Guid AdminUserId,
    string Email,
    string FullName,
    string Status,
    Guid? TeamId,
    DateTime OccurredOnUtc);
}
