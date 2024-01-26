using DemoBlazorWASM.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace DemoBlazorWASM.Pages.Movies
{
    public partial class MovieDetail
    {
        [Parameter]
        public int Id { get; set; }

        public Movie currentMovie { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7152/api/");

            using (HttpResponseMessage message = await client.GetAsync("movie/getbyid/"+Id))
            {
                message.EnsureSuccessStatusCode();
                string json = await message.Content.ReadAsStringAsync();
                currentMovie = JsonConvert.DeserializeObject<Movie>(json);
                StateHasChanged();
            }
        }
    }
}
