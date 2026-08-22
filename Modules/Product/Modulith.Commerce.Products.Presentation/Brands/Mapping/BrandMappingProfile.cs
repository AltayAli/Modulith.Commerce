using AutoMapper;
using Modulith.Commerce.Products.Application.Brands.Commands.AddBrand;
using Modulith.Commerce.Products.Application.Brands.Commands.UpdateBrand;
using Modulith.Commerce.Products.Application.Brands.Queries.GetBrand;
using Modulith.Commerce.Products.Application.Brands.Queries.GetBrands;
using Modulith.Commerce.Products.Presentation.Brands.DTOs;

namespace Modulith.Commerce.Products.Presentation.Brands.Mapping
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<BrandRequestDto, AddBrandCommand>();
            CreateMap<BrandRequestDto, UpdateBrandCommand>();
            CreateMap<GetBrandsResponse, BrandResponseDto>();
            CreateMap<GetBrandResponse, BrandResponseDto>();
        }
    }
}
