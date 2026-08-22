using Modulith.Commerce.API.OpenApi;

namespace Modulith.Commerce.API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddModuleConfiguration(this IConfigurationBuilder buider)
        {
            var modules = ModuleRegistry.BuildModules().Select(m => m.Key).ToArray();
            foreach (var module in modules)
            {
                buider.AddJsonFile($"module.{module}.json", false, true);
                buider.AddJsonFile($"module.{module}.Development.json", true, true);
            }
        }
    }
}
