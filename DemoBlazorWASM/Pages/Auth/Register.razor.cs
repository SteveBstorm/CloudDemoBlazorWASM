using DemoBlazorWASM.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text;

namespace DemoBlazorWASM.Pages.Auth
{
    public partial class Register
    {
        [Inject]
        public NavigationManager navManager { get; set; }


        private HttpClient _client;

        private string _url = "https://localhost:7152/api/user/";

        public string ResolveMessage { get; set; }

        public RegisterForm myForm { get; set; }
        public Register()
        {
            myForm = new RegisterForm();
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
        }

        public async Task OnSubmit()
        {
            string json = JsonConvert.SerializeObject(myForm);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using(HttpResponseMessage message = await _client.PostAsync("register", content))
            {
                if (message.IsSuccessStatusCode)
                {
                    ResolveMessage = "Enregistrement effectué avec succès \n Vous allez être rediriger";
                    StateHasChanged();
                    await Task.Delay(2000);
                    navManager.NavigateTo("login");
                }
                else
                {
                    ResolveMessage = "Le dev a encore du foiré un truc, faut recommencer";
                }
            }
        }

    }
}
