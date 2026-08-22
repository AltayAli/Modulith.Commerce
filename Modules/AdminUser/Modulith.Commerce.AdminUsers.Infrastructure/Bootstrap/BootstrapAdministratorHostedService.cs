using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser;
using Modulith.Commerce.Common.Domain.Abstractions;
using System.Security.Cryptography;
using AdminUserEntity = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Bootstrap
{
    public class BootstrapAdministratorHostedService(
    IServiceScopeFactory scopeFactory,
    IOptions<BootstrapAdministratorOptions> options,
    ILogger<BootstrapAdministratorHostedService> logger) : IHostedService
    {

        private static readonly Guid AdministratorRoleId = new("a069c55c-6af7-4f47-b99c-a1816e5c40c2");

        private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(2);
        private const int MaxPollAttempts = 15;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await BootstrapAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Bootstrap administrator provisioning failed unexpectedly.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task BootstrapAsync(CancellationToken cancellationToken)
        {
            var settings = options.Value;

            if (string.IsNullOrWhiteSpace(settings.AdministratorEmail))
            {
                logger.LogWarning("Bootstrap:AdministratorEmail not configured, skipping administrator bootstrap.");
                return;
            }

            using var scope = scopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            var sender = services.GetRequiredService<ISender>();
            var adminUsersRepository = services.GetRequiredService<IAdminUsersRepository>();
            var adminUserRolesRepository = services.GetRequiredService<IAdminUserRolesRepository>();
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();

            string normalizedEmail = settings.AdministratorEmail.Trim().ToLower();

            AdminUserEntity? adminUser = await FindByEmailAsync(adminUsersRepository, normalizedEmail, cancellationToken);

            if (adminUser is null)
            {
                adminUser = await CreateAdministratorAsync(sender, adminUsersRepository, settings, normalizedEmail, cancellationToken);

                if (adminUser is null)
                    return;
            }

            if (adminUser.KeyCloakId is null)
            {
                adminUser = await WaitForKeycloakSyncAsync(adminUsersRepository, adminUser.Id, cancellationToken);
            }

            if (adminUser is null || adminUser.KeyCloakId is null)
            {
                logger.LogError(
                    "Administrator role henüz atanamadı, sonraki restart tekrar deneyecek. Bootstrap Administrator {Email} Keycloak sync did not complete in time (status: {Status}).",
                    normalizedEmail,
                    adminUser?.Status);
                return;
            }

            await AssignAdministratorRoleAsync(adminUserRolesRepository, unitOfWork, adminUser, normalizedEmail, cancellationToken);
        }

        private static Task<AdminUserEntity?> FindByEmailAsync(IAdminUsersRepository repository, string normalizedEmail, CancellationToken cancellationToken) =>
            repository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserEntity>
            {
                Predicates = [u => u.Email.Value.ToLower() == normalizedEmail]
            }, cancellationToken);

        private async Task<AdminUserEntity?> CreateAdministratorAsync(
            ISender sender,
            IAdminUsersRepository adminUsersRepository,
            BootstrapAdministratorOptions settings,
            string normalizedEmail,
            CancellationToken cancellationToken)
        {
            string password = settings.AdministratorPassword ?? string.Empty;

            if (string.IsNullOrWhiteSpace(password))
            {
                password = GenerateSecurePassword();

                logger.LogWarning(
                    "No Bootstrap:AdministratorPassword configured — generated a random password for the bootstrap Administrator account ({Email}): {Password}. CHANGE THIS PASSWORD IMMEDIATELY.",
                    normalizedEmail,
                    password);
            }

            var command = new AddAdminUserCommand
            {
                Email = settings.AdministratorEmail!.Trim(),
                FirstName = settings.AdministratorFirstName,
                LastName = settings.AdministratorLastName,
                Title = "Administrator",
                PhoneNumber = null,
                AvatarUrl = null,
                ContractStartDate = DateTime.UtcNow,
                MfaEnabled = false,
                Password = password
            };

            Result result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                logger.LogError(
                    "Failed to create bootstrap Administrator account ({Email}): {ErrorCode} - {ErrorName}",
                    normalizedEmail,
                    result.Error?.Code,
                    result.Error?.Name);
                return null;
            }

            var adminUser = await FindByEmailAsync(adminUsersRepository, normalizedEmail, cancellationToken);

            if (adminUser is null)
            {
                logger.LogError(
                    "Bootstrap Administrator account ({Email}) could not be found immediately after creation.",
                    normalizedEmail);
            }

            return adminUser;
        }

        private async Task<AdminUserEntity?> WaitForKeycloakSyncAsync(IAdminUsersRepository repository, Guid adminUserId, CancellationToken cancellationToken)
        {
            AdminUserEntity? adminUser = null;

            for (int attempt = 1; attempt <= MaxPollAttempts; attempt++)
            {
                adminUser = await repository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserEntity>
                {
                    Predicates = [u => u.Id == adminUserId]
                }, cancellationToken);

                bool syncSettled = adminUser is not null &&
                    (adminUser.KeyCloakId is not null || adminUser.Status == AdminUserStatus.KeycloakSyncFailed);

                if (syncSettled)
                    return adminUser;

                await Task.Delay(PollInterval, cancellationToken);
            }

            return adminUser;
        }

        private async Task AssignAdministratorRoleAsync(
            IAdminUserRolesRepository adminUserRolesRepository,
            IUnitOfWork unitOfWork,
            AdminUserEntity adminUser,
            string normalizedEmail,
            CancellationToken cancellationToken)
        {
            DateTime now = DateTime.UtcNow;

            bool hasActiveAdministratorRole = await adminUserRolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserRole>
            {
                Predicates =
                [
                    ur => ur.UserId == adminUser.Id &&
                          ur.RoleId == AdministratorRoleId &&
                          (ur.ExpiredAt == null || ur.ExpiredAt > now)
                ]
            }, cancellationToken) is not null;

            if (hasActiveAdministratorRole)
            {
                logger.LogInformation("Bootstrap Administrator {Email} already has the Administrator role assigned.", normalizedEmail);
                return;
            }

            var adminUserRole = AdminUserRole.Create(adminUser.Id, AdministratorRoleId, expiredAt: null, reason: "bootstrap");

            await adminUserRolesRepository.InsertAsync(adminUserRole, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Assigned the Administrator role to bootstrap admin user {Email}.", normalizedEmail);
        }

        private static string GenerateSecurePassword()
        {
            Span<byte> bytes = stackalloc byte[24];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
