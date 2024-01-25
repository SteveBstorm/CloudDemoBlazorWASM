using DemoBlazorWASM.Models;
using DemoBlazorWASM.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DemoBlazorWASM.Pages
{
    public partial class UserList
    {
        private HttpClient _client;
        private string _url = "https://localhost:7152/api/user/";

        [Inject]
        public IServiceProvider service { get; set; }

        [Inject]
        public IJSRuntime js { get; set; }

        public IEnumerable<User> userList { get; set; } = new List<User>();
        public UserList()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
        }

        protected async override Task OnInitializedAsync()
        {
            MyAuthProvider state = (MyAuthProvider)service.GetService<AuthenticationStateProvider>();

            if(state.CurrentUser != null)
            {
                string token = await js.InvokeAsync<string>("localStorage.getItem", "token");
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (HttpResponseMessage message = await _client.GetAsync(""))
                {
                    message.EnsureSuccessStatusCode();
                    string json = await message.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
                }
            }

           
        }
    }
}
