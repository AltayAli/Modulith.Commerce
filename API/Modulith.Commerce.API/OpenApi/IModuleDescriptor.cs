using Asp.Versioning;

namespace Modulith.Commerce.API.OpenApi;

public interface IModuleDescriptor
{
    string Key { get; }
    string Title { get; }
    IReadOnlyList<ApiVersion> Versions { get; }
    string? Description { get; }
    string GroupName(ApiVersion v) => $"{Key}-v{v.MajorVersion}";
}
