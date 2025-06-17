using Microsoft.AspNetCore.Components.Authorization;
using NsUserManager.Components;
using NsUserManager.Services;
using System.Security.Claims;

namespace NsUserManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration from environment variables
            builder.Configuration.AddEnvironmentVariables("APPSETTINGS__");

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddHttpClient();
            builder.Services.AddBlazorBootstrap();

            // Add SAML authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "Saml2";
            })
            .AddCookie("Cookies")
            .AddSaml2(options =>
            {
                var samlConfig = builder.Configuration.GetSection("Saml");
                options.SPOptions.EntityId = new Sustainsys.Saml2.Metadata.EntityId(samlConfig["EntityId"]);
                options.SPOptions.PublicOrigin = new Uri(samlConfig["PublicOrigin"]);
                options.IdentityProviders.Add(
                    new Sustainsys.Saml2.IdentityProvider(
                        new Sustainsys.Saml2.Metadata.EntityId(samlConfig["IdpId"]),
                        options.SPOptions)
                    {
                        MetadataLocation = samlConfig["MetadataUrl"],
                        LoadMetadata = true,
                        AllowUnsolicitedAuthnResponse = false
                    });
            });

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<IScimService, MockScimService>();
                //builder.Services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();
            }
            else
            {
                builder.Services.AddScoped<IScimService, ScimService>();
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            // Add authentication middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }

    public class MockAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "DevUser"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "mock");

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
