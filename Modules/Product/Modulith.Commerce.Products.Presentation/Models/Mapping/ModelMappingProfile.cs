using AutoMapper;
using Modulith.Commerce.Products.Application.Models.Commands.AddModel;
using Modulith.Commerce.Products.Application.Models.Commands.UpdateModel;
using Modulith.Commerce.Products.Application.Models.Queries.GetModels;
using Modulith.Commerce.Products.Presentation.Models.DTOs;

namespace Modulith.Commerce.Products.Presentation.Models.Mapping
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<ModelRequestDto, AddModelCommand>();
            CreateMap<ModelRequestDto, UpdateModelCommand>();
            CreateMap<GetModelsQueryResponse, ModelResponseDto>();
        }
    }
}
