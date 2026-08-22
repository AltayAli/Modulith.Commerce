using Modulith.Commerce.API.OpenApi;

namespace Modulith.Commerce.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddModulithCommerceSwagger(
        this IServiceCollection services,
        IReadOnlyList<IModuleDescriptor> modules)
    {
        services.AddSingleton(modules);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        return services;
    }

    public static WebApplication UseModulithCommerceSwaggerUI(
        this WebApplication app,
        IReadOnlyList<IModuleDescriptor> modules)
    {
        app.UseSwagger();
        app.UseSwaggerUI(ui =>
        {
            foreach (var module in modules)
            {
                foreach (var version in module.Versions)
                {
                    var groupName = module.GroupName(version);
                    ui.SwaggerEndpoint(
                        $"/swagger/{groupName}/swagger.json",
                        $"{module.Title} v{version.MajorVersion}");
                }
            }

            ui.ConfigObject.AdditionalItems["withCredentials"] = true;

            ui.HeadContent = """
                <style>
                  #modulith-commerce-login-bar {
                    position: fixed; top: 0; left: 0; right: 0; z-index: 10000;
                    background: #1b1b1b; color: #fff; padding: 8px 16px;
                    font-family: sans-serif; font-size: 13px;
                    display: flex; align-items: center; gap: 12px;
                  }
                  #modulith-commerce-login-bar a {
                    background: #49cc90; color: #1b1b1b; padding: 4px 12px;
                    border-radius: 4px; text-decoration: none; font-weight: 600;
                    cursor: pointer;
                  }
                  body { padding-top: 40px; }
                </style>
                <div id="modulith-commerce-login-bar">
                  <span id="modulith-commerce-login-status">Modulith Commerce API</span>
                </div>
                <script>
                  window.addEventListener("load", function () {
                    var status = document.getElementById("modulith-commerce-login-status");

                    function renderLoggedOut() {
                      status.innerHTML = "Modulith Commerce API "
                        + "<a href=\"/api/v1/auth/login?returnUrl=/swagger\">Keycloak ile giriş yap</a>";
                    }

                    function renderLoggedIn(user) {
                      var label = user.email || user.name || "giriş yapıldı";
                      status.innerHTML = "Modulith Commerce API — " + label + " "
                        + "<a id=\"modulith-commerce-logout-link\">Çıkış yap</a>";
                      document.getElementById("modulith-commerce-logout-link").addEventListener("click", function () {
                        // fetch() burada yanlış araç: logout, Keycloak'un end_session_endpoint'ine
                        // (farklı origin) çok adımlı redirect zinciri gerektiriyor - fetch bunu
                        // CORS altında takip edemiyor, .then() hiç tetiklenmiyor. Gerçek bir form
                        // POST navigation'ı browser'ın kendisine tüm zinciri (Keycloak dahil)
                        // normal bir link tıklaması gibi takip ettiriyor; sonunda RedirectUri
                        // (/swagger) tam sayfa reload ile geri geliyor, banner kendini yeniliyor.
                        var form = document.createElement("form");
                        form.method = "POST";
                        form.action = "/api/v1/auth/logout";
                        document.body.appendChild(form);
                        form.submit();
                      });
                    }

                    fetch("/api/v1/auth/me", { credentials: "include" })
                      .then(function (res) { return res.ok ? res.json().then(renderLoggedIn) : renderLoggedOut(); })
                      .catch(renderLoggedOut);
                  });
                </script>
                """;
        });
        return app;
    }
}
