using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;

public class CustomAuthenticationService : AuthenticationStateProvider
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    
    private AuthenticationState anonymousState = new AuthenticationState(new ClaimsPrincipal());
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {           
        if (!String.IsNullOrWhiteSpace(Username) && IsAuthenticated(Username, Password))
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
        await Task.CompletedTask;
    }

    public async Task SignInAsync(string username, string password)
    {
        Username = username;
        Password = password;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        await Task.CompletedTask;
    }

    private bool IsAuthenticated(string? username, string? password) => (username?.Equals("Batman") ?? false) && (password?.Equals("sponge") ?? false);
}