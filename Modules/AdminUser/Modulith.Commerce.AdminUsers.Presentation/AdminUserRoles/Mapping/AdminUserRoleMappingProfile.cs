using AutoMapper;
using Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.AssignRoleToUser;
using Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Queries.GetUserRoles;
using Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles.Mapping
{
    public class AdminUserRoleMappingProfile : Profile
    {
        public AdminUserRoleMappingProfile()
        {
            CreateMap<AssignRoleToUserRequestDto, AssignRoleToUserCommand>();
            CreateMap<GetUserRolesItemResponse, UserRoleListItemResponseDto>();
        }
    }
}
