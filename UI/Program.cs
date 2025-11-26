using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using UI.Extensions;
using UI.Models;
using UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.RegisterCustomServices();

builder.Services.AddScoped<IFileService, LocalFileService>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "keycloak";
    })
    .AddCookie()
    .AddOpenIdConnect("keycloak", options =>
    {
        var keycloakData = builder.Configuration.GetSection("Keycloak").Get<KeycloakData>();

        options.Authority = $"{keycloakData.Host}/auth/realms/{keycloakData.Realm}";
        options.ClientId = keycloakData.ClientId;
        options.ClientSecret = keycloakData.ClientSecret;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.Scope.Add("openid");
        options.SaveTokens = true;
        options.RequireHttpsMetadata = false;
        options.MetadataAddress =
            $"{keycloakData.Host}/realms/{keycloakData.Realm}/.well-known/openid-configuration";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", p => p.RequireRole("POWER-USER"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapStaticAssets();

app.MapRazorPages().RequireAuthorization("admin");

app.MapControllerRoute(
        "areas",
        "{area:exists}/{controller=Games}/{action=GetAll}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();