using AutoMapper;
using Modulith.Commerce.AdminUsers.Application.Departments.Commands.AddDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Commands.UpdateDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartments;
using Modulith.Commerce.AdminUsers.Presentation.Departments.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.Departments.Mapping
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<AddDepartmentRequestDto, AddDepartmentCommand>();
            CreateMap<UpdateDepartmentRequestDto, UpdateDepartmentCommand>();
            CreateMap<GetDepartmentsItemResponse, DepartmentListItemResponseDto>();
            CreateMap<GetDepartmentResponse, DepartmentDetailResponseDto>();
        }
    }
}
