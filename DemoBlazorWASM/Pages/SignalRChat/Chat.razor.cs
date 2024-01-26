
using Microsoft.AspNetCore.SignalR.Client;

namespace DemoBlazorWASM.Pages.SignalRChat
{
    public partial class Chat
    {
        public string message { get; set; }

        public List<string> mesMessages { get; set; }

        public HubConnection connection { get; set; }

        //Microsoft.AspNetCore.SignalR.Client
        protected override async Task OnInitializedAsync()
        {
            mesMessages = new List<string>();
            //Initialiser la connexion en précisant l'url du hub
            connection = new HubConnectionBuilder().WithUrl("https://localhost:7152/chathub").Build();

            
            //Démarrer la connexion
            await connection.StartAsync();

            //Expliquer au composant comment réagir à un event déclenché par le hub
            connection.On("NewMessage", (string message) => {
                mesMessages.Add(message);
                StateHasChanged();
            });
        }

        public async Task Send()
        {
            //Déclencher un event du hub
            await connection.SendAsync("SendMessage", message);
        }

        public async Task JoinGroup()
        {
            await connection.SendAsync("JoinGroup", "lesPignouf");

            connection.On("messageFromlesPignouf", (string message) =>
            {
                mesMessages.Add(message);
                StateHasChanged();
            });
        }

        public async Task SendToGroup()
        {
            //Déclencher un event du hub
            await connection.SendAsync("SendToGroup", "lesPignouf", message);
        }
    }
}
