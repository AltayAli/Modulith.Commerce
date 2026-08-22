using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Permissions
{
    public record Action : ValueObject
    {
        public Action(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("Action cannot be null or empty.", nameof(action));

            Value = action;
        }
        public string Value { get; }

        public static Action Read => new Action("READ");
        public static Action Write => new Action("WRITE");
        public static Action Delete => new Action("DELETE");
        public static Action Export => new Action("EXPORT");
        public static Action Approve => new Action("APPROVE");

        public static Action GetFromList(string action)
            => All.FirstOrDefault(x => x.Value == action) ??
               throw new ArgumentException($"Unknown action: {action}");
        public static IReadOnlyCollection<Action> All => [Read, Write, Delete, Export, Approve];

        public static implicit operator string(Action n) => n.Value;
        public static explicit operator Action(string n) => new Action(n);
    }
}
