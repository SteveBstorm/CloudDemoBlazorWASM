using DemoBlazorWASM;
using DemoBlazorWASM.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, MyAuthProvider>();

await builder.Build().RunAsync();

/*
 Pour mettre en place le state provider

    1) Ecrire la classe "MyAuthProvider" qui doit h�riter de AuthenticationStateProvider
        ne pas oublier la m�thode pour d�clencher la mise � jour du user courant
    2) Ajouter au program.cs AddAuthorizationCore() et
        enregistrer mon provider dans l'injection de d�pendance
    3) Modifier le fichier app.razor pour qu'il prenne en compte le state (en commentaire l'avant modif)
    4) Pr�ciser � chaque composant qui le n�c�ssite qu'il s'agit d'une <AuthorizeView>
   
    !!!ATTENTION!!!
    Ne pas oublier de d�clencher la m�thode NotifyUserChanged du provider quand il y a un changement d'�tat
    pour r�percuter sur l'enti�ret� de l'app englob�e dans <CascadingAuthenticationState> (app.razor)

     => Passage par le IServiceProvider pour acc�der � l'instance COURANTE de votre AuthProvider <=
 */