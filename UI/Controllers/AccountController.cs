using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UI.Authorization;
using UI.Models;
using UI.Services;

namespace UI.Controllers;

public class AccountController(HttpClient httpClient, IFileService fileService, ITokenAccessor tokenAccessor, IHttpContextAccessor httpContextAccessor, IOptions<KeycloakData> options) : Controller
{
    
    public IActionResult Register()
    {
        return View(new RegisterUserViewModel());
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> RegisterAsync(RegisterUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await tokenAccessor.SetAuthorizationHeaderAsync(httpClient, true);

        var uriBuilder = new UriBuilder($"{options.Value.Host}/admin/realms/{options.Value.Realm}/users");

        var uri = uriBuilder.Uri;
        var profilePictureUrl = "images/default-profile-picture.png";

        if (model.ProfilePicture is not null)
        {
            profilePictureUrl = await fileService.SaveFileAsync(model.ProfilePicture);
        }

        var createUserModel = new CreateUserModel
        {
            Email = model.Email,
            Username = model.Email
        };
        
        createUserModel.Attributes.Add("UserAvatarUrl", profilePictureUrl);
        createUserModel.Credentials.Add(new UserCredentials
        {
            Value = model.Password
        });

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var createUserModelSerialized = JsonSerializer.Serialize(createUserModel, serializerOptions);
        HttpContent content = new StringContent(createUserModelSerialized, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(uri, content);

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest(response.StatusCode);
        }

        return LocalRedirect("/Home/Index");
    }

    public async Task Login()
    {
        await HttpContext.ChallengeAsync( 
            "keycloak", 
            new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") 
            }); 
    }
    
    public async Task Logout()
    {
        await 
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
        await HttpContext.SignOutAsync("keycloak", 
            new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") 
            }); 
    }
}

internal class UserCredentials
{
    public string Type { get; set; } = "password";
    public bool Temporary { get; set; } = false;
    public string Value { get; set; } = string.Empty;
}

internal class CreateUserModel
{
    public Dictionary<string, string> Attributes { get; } = new();
    public string Username { get; set; }
    public string Email { get; set; }
    public bool Enabled { get; set; } = true;
    public bool EmailVerified { get; set; } = true;
    public List<UserCredentials> Credentials { get; set; } = [];
}