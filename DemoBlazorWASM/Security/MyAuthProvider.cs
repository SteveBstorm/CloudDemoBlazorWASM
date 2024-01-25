using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DemoBlazorWASM.Security
{
    public class MyAuthProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;

        public ClaimsPrincipal CurrentUser { get; set; }

        public MyAuthProvider(IJSRuntime js)
        {
            _js = js;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            if(string.IsNullOrEmpty(token))
            {
                CurrentUser = null;
                ClaimsIdentity anonymousUser = new ClaimsIdentity();
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));
            }

            JwtSecurityToken jwt = new JwtSecurityToken(token);

            ClaimsIdentity currentUser = new ClaimsIdentity(jwt.Claims, "MyAuthType");
            CurrentUser = new ClaimsPrincipal(currentUser);
            Task<AuthenticationState> myTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(currentUser)));
            //NotifyAuthenticationStateChanged(myTask);
            return await myTask;

        }

        public void NotifyUserChange()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
