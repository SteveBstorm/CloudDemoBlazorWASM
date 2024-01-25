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

    1) Ecrire la classe "MyAuthProvider" qui doit hériter de AuthenticationStateProvider
        ne pas oublier la méthode pour déclencher la mise à jour du user courant
    2) Ajouter au program.cs AddAuthorizationCore() et
        enregistrer mon provider dans l'injection de dépendance
    3) Modifier le fichier app.razor pour qu'il prenne en compte le state (en commentaire l'avant modif)
    4) Préciser à chaque composant qui le nécéssite qu'il s'agit d'une <AuthorizeView>
   
    !!!ATTENTION!!!
    Ne pas oublier de déclencher la méthode NotifyUserChanged du provider quand il y a un changement d'état
    pour répercuter sur l'entièreté de l'app englobée dans <CascadingAuthenticationState> (app.razor)

     => Passage par le IServiceProvider pour accéder à l'instance COURANTE de votre AuthProvider <=
 */