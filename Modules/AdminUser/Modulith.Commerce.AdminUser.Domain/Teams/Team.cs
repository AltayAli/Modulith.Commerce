using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modulith.Commerce.AdminUser.Domain.Teams
{
    public class Team : BaseEntity
    {
        private Team() { }

        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }
        public Text Name { get; private set; }
        public Guid LeadId { get; private set; }
        public AdminUsers.AdminUser Lead { get; private set; }

        public static Team Create(string name, Guid departmentId, Guid leadId)
        {
            return new Team
            {
                Name = (Text)name,
                DepartmentId = departmentId,
                LeadId = leadId
            };
        }

        public Team Update(string name, Guid departmentId, Guid leadId)
        {
            Name = (Text)name;
            DepartmentId = departmentId;
            LeadId = leadId;
            return this;
        }
    }
}
