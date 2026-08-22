using AutoMapper;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.UpdateAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUsers;
using Modulith.Commerce.AdminUsers.Presentation.AdminUsers.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUsers.Mapping
{
    public class AdminUserMappingProfile : Profile
    {
        public AdminUserMappingProfile()
        {
            CreateMap<AddAdminUserRequestDto, AddAdminUserCommand>();
            CreateMap<UpdateAdminUserRequestDto, UpdateAdminUserCommand>();
            CreateMap<GetAdminUsersItemResponse, AdminUserListItemResponseDto>();
            CreateMap<GetAdminUserResponse, AdminUserDetailResponseDto>();
        }
    }
}
