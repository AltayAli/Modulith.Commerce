using AutoMapper;
using Modulith.Commerce.AdminUsers.Application.Roles.Commands.AddRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Commands.UpdateRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRoles;
using Modulith.Commerce.AdminUsers.Presentation.Roles.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.Roles.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<AddRoleRequestDto, AddRoleCommand>();
            CreateMap<UpdateRoleRequestDto, UpdateRoleCommand>();
            CreateMap<GetRolesItemResponse, RoleListItemResponseDto>();
            CreateMap<GetRoleResponse, RoleDetailResponseDto>();
        }
    }
}
