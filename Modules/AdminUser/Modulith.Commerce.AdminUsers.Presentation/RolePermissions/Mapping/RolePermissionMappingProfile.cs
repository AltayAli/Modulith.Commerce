using AutoMapper;
using Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.AddRolePermission;
using Modulith.Commerce.AdminUsers.Application.RolePermissions.Queries.GetRolePermissions;
using Modulith.Commerce.AdminUsers.Presentation.RolePermissions.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.RolePermissions.Mapping
{
    public class RolePermissionMappingProfile : Profile
    {
        public RolePermissionMappingProfile()
        {
            CreateMap<AddRolePermissionRequestDto, AddRolePermissionCommand>();
            CreateMap<GetRolePermissionsItemResponse, RolePermissionListItemResponseDto>();
        }
    }
}
