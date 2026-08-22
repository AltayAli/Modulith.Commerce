using Asp.Versioning;
using Modulith.Commerce.API.Extensions;
using Modulith.Commerce.API.Middlewares;
using Modulith.Commerce.API.OpenApi;
using Modulith.Commerce.API.OpenApi.AdminUsers;
using Modulith.Commerce.API.OpenApi.Products;
using Modulith.Commerce.AdminUsers.Infrastructure;
using Modulith.Commerce.AdminUsers.Presentation;
using Modulith.Commerce.AdminUsers.Presentation.Auth;
using Modulith.Commerce.Common.Auth;
using Modulith.Commerce.Common.Infrastructure;
using Modulith.Commerce.Common.Infrastructure.Extensions;
using Modulith.Commerce.Common.Infrastructure.Messaging;
using Modulith.Commerce.Products.Infrastructure;
using Modulith.Commerce.Products.Infrastructure.Data;
using Serilog;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddModulithCommerceAuthentication(builder.Configuration);
builder.Services.AddKeycloakClient(builder.Configuration);

builder.Services.AddApplicationConfiguration(builder.Configuration, new[]
{
    typeof(Modulith.Commerce.Products.Application.AssemblyReference).Assembly,
    typeof(Modulith.Commerce.AdminUsers.Application.AssemblyReference).Assembly
});

builder.Services.AddProductModule(builder.Configuration);
builder.Services.AddAdminUserModule(builder.Configuration);

builder.Services.AddModulithCommerceMessaging(typeof(Modulith.Commerce.Products.Infrastructure.AssemblyReference).Assembly);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var modules = ModuleRegistry.BuildModules();
builder.Services.AddModulithCommerceSwagger(modules);

builder.Configuration.AddModuleConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseModulithCommerceSwaggerUI(modules);
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.ApplyModuleMigrations<ProductsDbContext>();
app.ApplyModuleMigrations<AdminUsersDbContext>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

foreach (var module in modules)
{
    var versionSet = app.CreateModuleVersionSet(module);

    foreach (var version in module.Versions)
    {
        var versionedGroup = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(version)
            .WithGroupName(module.GroupName(version));

        switch (module)
        {
            case ProductsModuleDescriptor:
                versionedGroup.MapProductModuleEndpoints();
                break;
            case AdminUsersModuleDescriptor:
                versionedGroup.MapAdminUserModuleEndpoints();
                break;
        }
    }
}

app.Run();
