using DemoBlazorWASM.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace DemoBlazorWASM.Pages.Auth
{
    public partial class Logout
    {
        [Inject]
        public IServiceProvider service { get; set; }
        [Inject]
        public IJSRuntime js { get; set; }
        [Inject]
        public NavigationManager nav { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await js.InvokeVoidAsync("localStorage.clear");
            ((MyAuthProvider)service.GetService<AuthenticationStateProvider>()).NotifyUserChange();
            StateHasChanged();
            nav.NavigateTo("/");
            
        }
    }
}
