using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;

namespace Modulith.Commerce.AdminUser.Domain.AdminUsers
{
    public class AdminUser : BaseEntity
    {
        private AdminUser()
        {
            Departments = new HashSet<Department>();
            Teams = new HashSet<Team>();
            UserRoles = new HashSet<AdminUserRole>();
        }
        public Guid? KeyCloakId { get; private set; }
        public Email Email { get; private set; }
        public Text FirstName { get; set; }
        public Text LastName { get; private set; }
        public Text Title { get; private set; }
        public Guid? TeamId { get; private set; }
        public Team Team { get; private set; }
        public AdminUserStatus Status { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
        public FileUrl? AvatarUrl { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime? ContractEndDate { get; private set; }
        public DateTime? OffboardedAt { get; private set; }
        public bool MfaEnabled { get; private set; }
        public ICollection<Department> Departments { get; private set; }
        public ICollection<Team> Teams { get; private set; }
        public ICollection<AdminUserRole> UserRoles { get; private set; }

        public void AssignToTeam(Guid teamId)
        {
            TeamId = teamId;
            RaiseTeamChangedEvent();
        }

        public void RemoveFromTeam()
        {
            TeamId = null;
            RaiseTeamChangedEvent();
        }

        public void TransferToTeam(Guid teamId)
        {
            TeamId = teamId;
            RaiseTeamChangedEvent();
        }

        private void RaiseTeamChangedEvent() => AddDomainEvent(new AdminUserUpdatedEvent(
            Id,
            Email.Value,
            FirstName.Value,
            LastName.Value,
            Status,
            TeamId,
            KeyCloakId));

        public static AdminUser Create(
            string email,
            string firstName,
            string lastName,
            string title,
            string? phoneNumber,
            string? avatarUrl,
            DateTime contractStartDate,
            bool mfaEnabled,
            string password)
        {
            var adminUser = new AdminUser
            {
                Id = Guid.NewGuid(),
                Email = (Email)email,
                FirstName = (Text)firstName,
                LastName = (Text)lastName,
                Title = (Text)title,
                PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : (PhoneNumber)phoneNumber,
                AvatarUrl = string.IsNullOrWhiteSpace(avatarUrl) ? null : new FileUrl(avatarUrl),
                Status = AdminUserStatus.PendingKeycloakSync,
                ContractStartDate = contractStartDate,
                MfaEnabled = mfaEnabled
            };

            adminUser.AddDomainEvent(new AdminUserCreatedEvent(
                adminUser.Id,
                adminUser.Email.Value,
                adminUser.FirstName.Value,
                adminUser.LastName.Value,
                adminUser.Status,
                adminUser.TeamId,
                password));

            return adminUser;
        }

        public void SetKeycloakId(Guid keycloakId)
        {
            KeyCloakId = keycloakId;
            Status = AdminUserStatus.Active;
        }

        public void MarkKeycloakSyncFailed()
        {
            Status = AdminUserStatus.KeycloakSyncFailed;
        }

        public AdminUser Update(
            string firstName,
            string lastName,
            string title,
            string? phoneNumber,
            string? avatarUrl,
            AdminUserStatus status,
            DateTime? contractEndDate,
            bool mfaEnabled)
        {
            FirstName = (Text)firstName;
            LastName = (Text)lastName;
            Title = (Text)title;
            PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : (PhoneNumber)phoneNumber;
            AvatarUrl = string.IsNullOrWhiteSpace(avatarUrl) ? null : new FileUrl(avatarUrl);
            Status = status;
            ContractEndDate = contractEndDate;
            MfaEnabled = mfaEnabled;

            AddDomainEvent(new AdminUserUpdatedEvent(
                Id,
                Email.Value,
                FirstName.Value,
                LastName.Value,
                Status,
                TeamId,
                KeyCloakId));

            return this;
        }

        public AdminUser RetryKeycloakSync(bool mfaEnabled)
        {
            MfaEnabled = mfaEnabled;
            Status = AdminUserStatus.PendingKeycloakSync;

            return this;
        }

        public override void MarkAsDeleted(Guid? deletedBy, DateTime deletedDate)
        {
            base.MarkAsDeleted(deletedBy, deletedDate);
            AddDomainEvent(new AdminUserDeletedEvent(Id, Email.Value, KeyCloakId));
        }
    }
}
