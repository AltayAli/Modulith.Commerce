namespace Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers
{
    public sealed record AdminUserDeletedIntegrationEvent(
    Guid AdminUserId,
    DateTime OccurredOnUtc);
}
