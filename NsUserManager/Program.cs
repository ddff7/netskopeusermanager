using NsUserManager.Components;
using NsUserManager.Services;

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

            builder.Services.AddHttpClient();
            builder.Services.AddBlazorBootstrap();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<IScimService, MockScimService>();
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

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
