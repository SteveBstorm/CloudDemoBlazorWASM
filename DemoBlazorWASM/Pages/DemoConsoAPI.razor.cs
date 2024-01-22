using DemoBlazorWASM.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DemoBlazorWASM.Pages
{
    public partial class DemoConsoAPI
    {
        /*
         Tout ce qui est en commentaire concerne la durée de vie des composant
         */

        private string url = "https://localhost:7152/api/";
        
        [Inject]
        public HttpClient client { get; set; }

        public List<Movie> MaListe { get; set; }

        public DemoConsoAPI()
        {
            MaListe = new List<Movie>();
            Console.WriteLine("Constructor");
            //number = 5;
        }
        protected override async Task OnInitializedAsync()
        {
            client.BaseAddress = new Uri(url);
            using(HttpResponseMessage message = await client.GetAsync("movie"))
            {
                if(message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    MaListe = JsonConvert.DeserializeObject<List<Movie>>(json);
                }
            }
            await Console.Out.WriteLineAsync("Initialized");
        }

        //protected override Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if(firstRender)
        //    {
        //        Console.WriteLine("Salut je vaut : " + number);
        //    }
        //    Console.WriteLine("valeur : " + number);
            
        //    return base.OnAfterRenderAsync(firstRender);
        //}

        //protected override bool ShouldRender()
        //{
        //    if (number == 10) { return false; }
        //    return base.ShouldRender();
        //}

        //public int number { get; set; }
        //public void Increment()
        //{
        //    number++;
        //    ShouldRender();
        //}
    }
}
