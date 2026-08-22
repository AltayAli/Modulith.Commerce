using AutoMapper;
using Modulith.Commerce.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty;
using Modulith.Commerce.Products.Application.CategoryProperties.Queries.GetCategoryProperties;
using Modulith.Commerce.Products.Presentation.CategoryProperties.DTOs;

namespace Modulith.Commerce.Products.Presentation.CategoryProperties.Mapping
{
    public class CategoryPropertyMappingProfile : Profile
    {
        public CategoryPropertyMappingProfile()
        {
            CreateMap<SaveCategoryPropertyItemDto, UpdateCategoryPropertyCommandItem>();

            CreateMap<SaveCategoryPropertyValueDto, UpdateCategoryPropertyValueItem>();

            CreateMap<GetCategoryProperiesResponse, CategoryPropertyResponseDto>();
        }
    }
}
