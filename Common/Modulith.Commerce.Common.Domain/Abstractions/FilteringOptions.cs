using System.Linq.Expressions;

namespace Modulith.Commerce.Common.Domain.Abstractions
{
    public sealed class FilteringOptions<T_Entity>
    {
        public FilteringOptions()
        {
            Relations = new List<string>();
            Predicates = new List<Expression<Func<T_Entity, bool>>>();
        }
        public List<Expression<Func<T_Entity, bool>>> Predicates { get; set; }
        public List<string> Relations { get; set; }
        public bool IsLoadingAsNoTracking { get; set; } = true;
        public bool IsSearchForAll { get; set; } = false;
    }
}
