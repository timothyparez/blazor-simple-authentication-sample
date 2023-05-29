using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;

public class CustomAuthenticationService : AuthenticationStateProvider
{
    public string Username { get; set; }
    public string Password { get; set; }
    
    private AuthenticationState anonymousState = new AuthenticationState(new ClaimsPrincipal());
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine($"Getting the authentication state: {Username}/{Password}");
        
        if (IsAuthenticated(Username, Password))
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Username) }, "Custom Authentication Type");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            return await Task.FromResult(state);
        }
        else
        {
            return anonymousState;
        }
    }

    public async Task SignOutAsync()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(anonymousState));
    }

    public async Task SignInAsync(string username, string password)
    {
        Username = username;
        Password = password;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private bool IsAuthenticated(string username, string password) => username == "Batman" && password == "sponge";
}