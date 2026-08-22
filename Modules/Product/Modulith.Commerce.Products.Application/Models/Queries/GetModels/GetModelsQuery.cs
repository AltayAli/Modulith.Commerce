using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Models.Queries.GetModels
{
    public class GetModelsQuery : IQuery<List<GetModelsQueryResponse>>
    {
        public Guid BrandId { get; set; }
    }
}
