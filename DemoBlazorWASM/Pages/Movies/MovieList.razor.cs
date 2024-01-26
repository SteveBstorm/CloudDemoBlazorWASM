using DemoBlazorWASM.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace DemoBlazorWASM.Pages.Movies
{
    public partial class MovieList
    {
        public List<Movie> Liste{ get; set; }

        HubConnection connection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            connection = new HubConnectionBuilder().WithUrl("https://localhost:7152/moviehub").Build();
            await connection.StartAsync();

            connection.On("NotifyNewMovie", async () => { await GetMovies(); });

            await GetMovies();
        }

        public async Task GetMovies()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7152/api/");

            using(HttpResponseMessage message = await client.GetAsync("movie"))
            {
                message.EnsureSuccessStatusCode();
                string json = await message.Content.ReadAsStringAsync();
                Liste = JsonConvert.DeserializeObject<List<Movie>>(json);
                StateHasChanged();
            }
        }

        public int SelectedId { get; set; }
        public void Detail(int id)
        {
            SelectedId = id;
        }

        
    }
}
