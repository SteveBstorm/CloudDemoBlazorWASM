using DemoBlazorWASM.Models;
using DemoBlazorWASM.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Text;

namespace DemoBlazorWASM.Pages.Auth
{
    public partial class Login
    {
        [Inject]
        public NavigationManager navManager { get; set; }

        [Inject]
        public IJSRuntime js { get; set; }

        [Inject]
        public IServiceProvider service{ get; set; }

        private HttpClient _client;

        private string _url = "https://localhost:7152/api/user/";

        public string ResolveMessage { get; set; }

        public LoginForm myForm { get; set; }
        public Login()
        {
            myForm = new LoginForm();
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
        }

        public async Task OnSubmit()
        {
            string json = JsonConvert.SerializeObject(myForm);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = await _client.PostAsync("login", content))
            {
                if (message.IsSuccessStatusCode)
                {
                    string token = await message.Content.ReadAsStringAsync();
                    await js.InvokeVoidAsync("localStorage.setItem", "token", token);
                    ResolveMessage = "Connexion effectué avec succès \n Vous allez être rediriger";
                    
                    ((MyAuthProvider)service.GetService<AuthenticationStateProvider>()).NotifyUserChange();
                    StateHasChanged();
                    navManager.NavigateTo("userlist");
                }
                else
                {
                    ResolveMessage = "Le dev a encore du foiré un truc, faut recommencer";
                }
            }
        }
    }
}
