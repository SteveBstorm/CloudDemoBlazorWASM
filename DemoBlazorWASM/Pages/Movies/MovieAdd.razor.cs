using DemoBlazorWASM.Models;
using Newtonsoft.Json;
using System.Text;

namespace DemoBlazorWASM.Pages.Movies
{
    public partial class MovieAdd
    {
        public Movie form { get; set; }
        public MovieAdd()
        {
            form = new Movie();
        }

        public async Task Submit()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7152/api/");

            string json = JsonConvert.SerializeObject(form);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = await client.PostAsync("movie", content))
            {
                message.EnsureSuccessStatusCode();
                form = new Movie();
            }
        }
    }
}
