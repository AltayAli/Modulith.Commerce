using AutoMapper;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddTeam;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.UpdateTeam;
using Modulith.Commerce.AdminUsers.Presentation.Teams.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.Teams.Mapping
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
            CreateMap<AddTeamRequestDto, AddTeamCommand>();
            CreateMap<UpdateTeamRequestDto, UpdateTeamCommand>();
        }
    }
}
