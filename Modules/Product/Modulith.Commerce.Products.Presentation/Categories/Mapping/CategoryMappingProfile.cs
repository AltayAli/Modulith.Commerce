using AutoMapper;
using Modulith.Commerce.Products.Application.Categories.Commands.AddCategory;
using Modulith.Commerce.Products.Application.Categories.Commands.UpdateCategory;
using Modulith.Commerce.Products.Application.Categories.Queries.GetCategories;
using Modulith.Commerce.Products.Presentation.Categories.DTOs;

namespace Modulith.Commerce.Products.Presentation.Categories.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<AddCategoryRequestDto, AddCategoryCommand>();
            CreateMap<UpdateCategoryRequestDto, UpdateCategoryCommand>();
            CreateMap<GetCategoriesItemResponse, CategoryResponseDto>();
        }
    }
}
